using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;

namespace Tony_Backend.Shared.Helpers
{
    public class QueueOperations : IQueueOperations
    {
        private static AmazonSQSClient _sqsClient;
        // TODO: use variable from appsetting
        private static string _connectionString = "https://sqs.eu-west-1.amazonaws.com/240595528763/clod-pw-g2-tony-queue-1.fifo";

        private static readonly string groupId = "to-gateway";

        public QueueOperations(IConfiguration configuration)
        {
            var credentials = LoadAWSCredentials();
            _sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.EUWest1);
            //_connectionString = configuration.GetConnectionString("send_queue_url");
        }

        public async Task SendMessage(string message)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _connectionString,
                MessageBody = message,
                MessageGroupId = groupId, 
                MessageDeduplicationId = Guid.NewGuid().ToString() 
            };

            var sendMessageResponse = await _sqsClient.SendMessageAsync(sendMessageRequest);

            if (sendMessageResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Message sent successfully: {message}");
            }
            else
            {
                Console.WriteLine($"Failed to send message: {message}");
            }
        }

        public static AWSCredentials LoadAWSCredentials()
        {
            // combine root paath to folder".aws" and credentials file
            var credentialsFilePath = Path.Combine("/app", ".aws", "credentials");
            var lines = File.ReadAllLines(credentialsFilePath);
             
            var profileName = "default";
            var accessKeyId = string.Empty;
            var secretAccessKey = string.Empty;

            var isInDefaultProfile = false;
            foreach (var line in lines)
            {
                if (line.Trim().Equals($"[{profileName}]", StringComparison.OrdinalIgnoreCase))
                {
                    isInDefaultProfile = true;
                    continue;
                }

                if (isInDefaultProfile)
                {
                    if (line.Trim().StartsWith("["))
                    {
                        break; // End of default profile section
                    }

                    var keyValue = line.Split(new[] { '=' }, 2);
                    if (keyValue.Length == 2)
                    {
                        var key = keyValue[0].Trim();
                        var value = keyValue[1].Trim();
                        if (key.Equals("aws_access_key_id", StringComparison.OrdinalIgnoreCase))
                        {
                            accessKeyId = value;
                        }
                        else if (key.Equals("aws_secret_access_key", StringComparison.OrdinalIgnoreCase))
                        {
                            secretAccessKey = value;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(accessKeyId) || string.IsNullOrEmpty(secretAccessKey))
            {
                throw new InvalidOperationException("AWS credentials not found in the specified profile.");
            }

            return new BasicAWSCredentials(accessKeyId, secretAccessKey);
        }
    }
}
