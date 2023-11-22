using Couche_Data.Interfaces;
using Modeles;
using System.Threading.Tasks.Dataflow;

namespace GalliumPlusApi.Dao
{
    internal class CachedLogReader : IPaginatedLogReader
    {
        private List<Log> allLogs;
        private BufferBlock<Log> buffer;
        private int pageSize;
        private int pageIndex;
        private bool exhausted;
        private object loadLock;

        public CachedLogReader(int pageSize, List<Log> allLogs)
        {
            this.allLogs = allLogs;
            this.buffer = new BufferBlock<Log>();
            this.pageSize = pageSize;
            this.pageIndex = 0;
            this.exhausted = false;
            this.loadLock = new object();
        }

        public int PageSize => this.pageSize;

        public IAsyncEnumerable<Log> GetAsyncStream(CancellationToken ct = default) => this.buffer.ReceiveAllAsync(ct);

        public void LoadNextPage()
        {
            lock (this.loadLock)
            {
                if (!this.exhausted)
                {
                    int pageStart = this.pageIndex * this.pageSize;
                    int pageEnd = pageStart + this.pageSize;

                    if (pageEnd > this.allLogs.Count)
                    {
                        pageEnd = this.allLogs.Count;
                        this.exhausted = true;
                    }
                    else
                    {
                        this.pageIndex++;
                    }

                    for (int i = pageStart; i < pageEnd; i++)
                    {
                        this.buffer.Post(this.allLogs[i]);
                    }
                }
            }
        }

        public void Reset()
        {
            // désactiver l'ancien tampon et en créer un nouveau
            this.buffer.Complete();
            this.buffer = new BufferBlock<Log>();

            // remettre le curseur au début
            this.pageIndex = 0;
            this.exhausted = false;
        }
    }
}
