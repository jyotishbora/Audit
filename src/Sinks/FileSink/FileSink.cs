using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAS.Audit;
using IAS.Audit.Abstractions;
using Newtonsoft.Json;

namespace IAS.Audit.Sinks.FileSink
{
    public class FileSink : IAuditSink
    {
        private readonly string _filePath;

        public FileSink(string filePath)
        {
            _filePath = filePath;
        }

        public async Task Dispatch(AuditEvent auditEvent)
        {
            await File.AppendAllTextAsync(_filePath, JsonConvert.SerializeObject(auditEvent) + "\r\n");
        }
    }
}
