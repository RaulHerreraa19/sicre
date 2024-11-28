using NLog;
using System;

namespace wsLogClass
{
    public static class exceptionHandlerCatch
    {
        /// <summary>
        /// Método para registrar una entrada de error de log
        /// </summary>
        /// <param name="ex"></param>
        public static void registerLogException(Exception ex)
        {
            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex, ex.Message);
            }
            catch { }
        }

        /// <summary>
        /// Método para registrar una entrada de alerta de log
        /// </summary>
        /// <param name="ex"></param>
        public static void registerLogWarning(Exception ex)
        {
            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(ex, ex.Message);
            }
            catch { }
        }

        /// <summary>
        /// Método para registrar una entrada de mensaje de log
        /// </summary>
        /// <param name="Message"></param>
        public static void registerMessage(string Message)
        {
            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Info(Message);
            }
            catch { }
        }
    }
}
