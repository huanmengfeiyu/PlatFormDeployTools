using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PlatFormDeployUtility
{
    /// <summary>
    /// C#文件操作类
    /// </summary>
    public class FileOperate
    {
        #region 文件操作

        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="fInfo"></param>
        /// <param name="newPath"></param>
        public static bool RenameFile(FileInfo fInfo, string newPath)
        {
            //string fullName = String.Format(LOG_FULLNAME, "RenameFile");
            Dictionary<string, string> logDic = new Dictionary<string, string>();
            logDic.Add("newPath", newPath);
            //LogMessageHelper.LogerMessage(LogMessageLevel.Info, fullName, string.Empty, "文件重命名", logDic);
            try
            {
                fInfo.MoveTo(newPath);
            }
            catch (Exception ex)
            {
                //LogMessageHelper.LogerMessage(LogMessageLevel.Error, fullName, string.Empty, "文件重命名错误", logDic, ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 目录重命名
        /// </summary>
        /// <param name="fInfo"></param>
        /// <param name="newPath"></param>
        public static bool RenameDir(string path, string newPath)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            Dictionary<string, string> logDic = new Dictionary<string, string>();
            logDic.Add("newPath", newPath);
            try
            {
                di.MoveTo(newPath);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        #endregion

        #region 文件夹操作
        /// <summary>
        /// 复制文件夹中的所有文件夹与文件到另一个文件夹
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }

        /// <summary>
        /// 移动文件夹中的所有文件夹与文件到另一个文件夹
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void MoveFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //覆盖模式
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }
                    File.Move(c, destFile);
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。
                    //Directory.Move(c, destDir);

                    //采用递归的方法实现
                    MoveFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }

        #region 删除指定目录下所有内容
        /// <summary>
        /// 删除指定目录下所有内容：方法一--删除目录，再创建空目录
        /// </summary>
        /// <param name="dirPath"></param>
        public static void DeleteFolderEx(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath);
                Directory.CreateDirectory(dirPath);
            }
        }

        /// <summary>
        /// 删除指定目录下所有内容：方法二--找到所有文件和子文件夹删除
        /// </summary>
        /// <param name="dirPath"></param>
        public static void DeleteFolder(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                foreach (string content in Directory.GetFileSystemEntries(dirPath))
                {
                    if (Directory.Exists(content))
                    {
                        Directory.Delete(content, true);
                    }
                    else if (File.Exists(content))
                    {
                        File.Delete(content);
                    }
                }
            }
        }
        #endregion
        #endregion
    }
}
