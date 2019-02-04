using System;

namespace Loggifyio.Api.Models
{
    public class CreateTrainingLog<T>
    {
        public T[] Data { get; set; }
        public int Total { get; set; }
    }

    public class CreateTrainingLog
    {
        public object Data { get; set; }
        public int Total { get; set; }
    }
}
