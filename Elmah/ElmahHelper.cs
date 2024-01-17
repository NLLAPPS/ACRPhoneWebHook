using ACRPhone.Webhook.AppSettings;

namespace ACRPhone.Webhook.Elmah
{

    public class ElmahHelper(ILogger<ElmahHelper> logger)
    {

        public int DeleteXmlLogFiles(ElmahConfig elmahConfig)
        {
            var totalDeleted = 0;
            var logPath = elmahConfig.LogFolder;
            var isLogPathExist = Directory.Exists(logPath);
            logger.LogDebug($"{nameof(DeleteXmlLogFiles)} -> logPath: {logPath}, isLogPathExist: {isLogPathExist}");
            if (isLogPathExist)
            {
                var xmlFiles = Directory.GetFiles(logPath, "*.xml", SearchOption.AllDirectories);
                foreach (var xmlFile in xmlFiles)
                {
                    try
                    {
                        File.Delete(xmlFile);
                        totalDeleted++;
                    }
                    catch (Exception e)
                    {

                        logger.LogDebug($"{nameof(DeleteXmlLogFiles)} -> Error when deleting: {e}");
                    }
                }
            }

            logger.LogDebug($"{nameof(DeleteXmlLogFiles)} -> xmlFiles: {totalDeleted}");
            return totalDeleted;

        }

    }
}
