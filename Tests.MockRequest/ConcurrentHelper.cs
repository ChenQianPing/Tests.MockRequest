using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests.MockRequest
{
    public class ConcurrentHelper
    {
        public int TaskId { get; private set; }

        public ConcurrentHelper(int seed)
        {
            TaskId = seed;
        }

        public int ReceiveData()
        {
            const string url = "http://192.168.2.100:9001/api/Rvw/GetImgPart/00AA44995CA94A6FB577E5D9A6F4B1E0/B477847E59AF4E03A37C12B15EFF6ADE";

            try
            {
                if (!System.IO.Directory.Exists(@"E:\10001003"))
                {
                    System.IO.Directory.CreateDirectory(@"E:\10001003");
                }

                using (var webclient = new WebClient())
                {
                    webclient.DownloadFile(url, $@"E:\10001003\{Guid.NewGuid():N}.jpg");
                    return TaskId;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> ReceiveDataAsync()
        {
            var task = new Task<int>(ReceiveData);
            task.Start();
            return await task;
        }
    }
}

