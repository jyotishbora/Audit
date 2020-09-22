using System;
using IAS.Audit.Sinks.UriSink;


namespace IAS.Audit.Extensions
{
    public static class UriSinkExtensions
    {
        public static AuditManagerBuilder WriteToUri(this AuditManagerBuilder builder, Action<UriSinkOptions> options)
        {
            
            UriSinkOptions o= new UriSinkOptions();
            options.Invoke(o);
            builder.AddSink(new UriSink(o.Uri));
            return builder;

        }
    }
}
