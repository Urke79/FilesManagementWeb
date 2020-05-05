using DataAccess;
using Microsoft.AspNet.SignalR;
using Quartz;
using System;
using System.IO;
using System.Linq;

namespace Web.SheduledTasks
{
    public class DeleteExpiredFilesJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            DeleteExpiredFiles();
            ReloadFilesTable();
        }

        private void ReloadFilesTable()
        {
            // SignalR - notify clients - refresh table
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ScheduledDeletionFilesHub>();
            hubContext.Clients.All.refreshTable();
        }

        private void DeleteExpiredFiles()
        {
            using (var db = new FileMetadataDbContext())
            {
                var expiredFiles = db.UploadedFileInfo.Where(f => f.Expires < DateTime.Now).ToList();

                foreach (var file in expiredFiles)
                {
                    try
                    {
                        db.UploadedFileInfo.Remove(file);
                        db.SaveChanges();

                        // delete file from the file system
                        File.Delete(file.Url);
                    }
                    catch (Exception e)
                    {
                        // TO DO - logging...
                    }
                }
            }
        }
    }
}