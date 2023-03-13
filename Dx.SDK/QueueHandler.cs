// Decompiled with JetBrains decompiler
// Type: Dx.SDK.QueueHandler
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace Dx.SDK
{
    internal class QueueHandler : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(QueueHandler));
        private ManualResetEvent waitForQueue = new ManualResetEvent(true);
        private ManualResetEvent stopReceived = new ManualResetEvent(false);
        protected Queue queue;
        protected Thread queueReader;
        private bool disposed;

        public ManualResetEvent WaitForQueue
        {
            get => this.waitForQueue;
            set => this.waitForQueue = value;
        }

        public event DequeuHandler QueueDequeued;

        public QueueHandler()
        {
            this.queue = QueueManager.GetQueue;
            this.queueReader = new Thread(new ThreadStart(this.ProcessQueue));
            this.queueReader.IsBackground = false;
            this.queueReader.Start();
        }

        public void Enqueue(object data)
        {
            lock (this.queue.SyncRoot)
            {
                this.queue.Enqueue(data);
                
                //Console.WriteLine("Command Enqueued in Queue handler, setting wait queue");
                this.waitForQueue.Set();
            }
        }

        public void ClearAll()
        {
            lock (this.queue.SyncRoot)
            {
                if (this.queue.Count <= 0)
                    return;
                this.queue.Clear();
            }
        }

        public void Resume()
        {
            if (this.queueReader != null || this.queueReader.IsAlive)
                return;
            this.stopReceived.Reset();
            this.waitForQueue.Reset();
            this.queueReader = new Thread(new ThreadStart(this.ProcessQueue));
            this.queueReader.IsBackground = false;
            this.queueReader.Start();
        }

        public int GetQueueItemCount
        {
            get
            {
                lock (this.queue.SyncRoot)
                    return this.queue.Count;
            }
        }

        public void Stop()
        {
            
            //Console.WriteLine("Stopping queue thread!!, calling set on stop received");
            this.stopReceived.Set();
            if (this.queueReader == null)
                return;
            this.queueReader.Abort();
        }

        private void ProcessQueue()
        {
            while (true)
            {
                object data = (object)null;
                lock (this.queue.SyncRoot)
                {
                    if (this.queue.Count > 0)
                        data = this.queue.Dequeue();
                    else
                        this.waitForQueue.Reset();
                }
                if (data != null)
                {
                    try
                    {
                        if (this.QueueDequeued != null)
                        {
                            
                            //Console.WriteLine("Dequeuing inside process queue");
                            this.QueueDequeued(data);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (QueueHandler.logger.IsErrorEnabled)
                            QueueHandler.logger.Error((object)"Error in Queuehandler:", ex);
                    }
                }
                else if (!this.stopReceived.WaitOne(0))
                    this.waitForQueue.WaitOne();
                else
                    break;
            }
            if (!QueueHandler.logger.IsDebugEnabled)
                return;
        //Console.WriteLine("Stop received exiting process queue");
        }

        public void Dispose()
        {
            this.stopReceived.Set();
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        private void Dispose(bool disposing)
        {
            
            //Console.WriteLine("Disposing queue handler!!!");
            if (!this.disposed && disposing)
            {
                if (this.queueReader.IsAlive)
                    this.queueReader.Abort();
                this.waitForQueue.Close();
            }
            this.disposed = true;
        }
    }
}
