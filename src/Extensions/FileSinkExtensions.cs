using System;
using IAS.Audit.Sinks.FileSink;


namespace IAS.Audit.Extensions
{
    public static class FileSinkExtensions
    {
        public static AuditManagerBuilder WriteToFile(this AuditManagerBuilder builder, Action<FileSinkOptions> optionAction)
        {

            FileSinkOptions fs = new FileSinkOptions();
            optionAction.Invoke(fs);
            builder.AddSink(new FileSink(fs.FilePath));
            return builder;
        }
    }
}
