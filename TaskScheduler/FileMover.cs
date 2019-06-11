using System.IO;

namespace TaskScheduler
{
    public class FileMover
    {
        public void MoveFile(object paths)
        {
            Pathes pathes = paths as Pathes;
            File.Move(pathes.From, pathes.To);
        }
    }
}
