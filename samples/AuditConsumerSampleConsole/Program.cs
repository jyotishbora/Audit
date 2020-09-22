using System;
using System.Net.Http;
using System.Threading.Tasks;
using AuditConsumerSampleConsole.Helper;
using IAS.Audit;
using IAS.Audit.Abstractions;
using IAS.Audit.Extensions;
using IAS.Audit.Scope;

namespace AuditConsumerSampleConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IAuditManager auditManager = new AuditManagerBuilder()
                .WriteToUri(options => { options.Uri = "https://localhost:5001/api/auditlogging"; })
                .WriteToFile(options => { options.FilePath = @"C:\Users\Jyotish\source\repos\IAS.Audit\samples\AuditConsumerSampleConsole\Audit.txt"; })
                .Build();

            for (int i = 0; i < 1; i++)
            {
                var auditEvent = AuditUtility.GenerateFakeMasterDealerAuditEvent();

                await auditManager.SaveAuditEvent(auditEvent);
            }
            
            Console.WriteLine("Branch info has been updated");
            Console.ReadLine();
        }
    }
}
