using System.IO;

namespace TaskScheduler
{
    public class FileMover
    {
        private void MoveFile(string from, string to)
        {
            File.Move(from, to);
        }
    }
}
