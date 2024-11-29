namespace Rogue_Warrior.Service.FileHandling
{
    /// <summary>
    /// Класс, отвечающий за хранение, получение и запись данных о различных путях.  
    /// </summary>
    public class PathProcessor
    {
        private string _baseDirectoryPath = "";
     
        public PathProcessor()
        {
            SetDirectoryPath();
        }
        
        /// <summary>
        /// Найти путь директории проекта и записать в переменную _baseDirectoryPath.
        /// </summary>
        private void SetDirectoryPath()
        {
            string[] directories = Directory.GetCurrentDirectory().Split(Path.DirectorySeparatorChar);
            _baseDirectoryPath = string.Join(Path.DirectorySeparatorChar, directories,
                0, directories.Length - 3) + Path.DirectorySeparatorChar;
        }
        
        /// <summary>
        /// Проверить файл на существование.
        /// </summary>
        /// <param name="path">Путь для проверки.</param>
        /// <returns>True - если файл существует, false - если нет.</returns>
        private bool DoesFileExist(string path)
        {
            return File.Exists(_baseDirectoryPath + path);
        }
        
        /// <summary>
        /// Проверка на то, является ли передаваемый путь валидным.
        /// </summary>
        /// <param name="path">Путь для проверки</param>
        /// <returns>True - если путь валидный, false - если нет.</returns>
        private static bool IsPathValid(string path)
        {

            if (path.Length == 0)
            {
                return false;
            }

            foreach (char ch in Path.GetInvalidPathChars())
            {
                if (!path.Contains(ch))
                {
                    continue;
                }

                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Получить полный путь файла.
        /// </summary>
        /// <returns>Абсолютный путь сохраненного входного файла.</returns>
        public string GetPath(string path)
        {
            return _baseDirectoryPath + path;
        }
    }
}