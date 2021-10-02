using Quartz;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class Job : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            bool result = false;

            try
            {
                MethodInfo method = typeof(Job).GetMethod(context.JobDetail.Key.Name);

                if (method != null)
                {
                    result = true;
                    var r = method.Invoke(this, Array.Empty<object>());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Task.FromResult(result);
        }

        public void Monthly()
        {
            System.Diagnostics.Debug.WriteLine("1. Job Başlatıldı");
        }
        public void Weekly()
        {
            System.Diagnostics.Debug.WriteLine("2. Job Başlatıldı");
        }
        public void Daily()
        {
            System.Diagnostics.Debug.WriteLine("3. Job Başlatıldı");
        }
    }
}