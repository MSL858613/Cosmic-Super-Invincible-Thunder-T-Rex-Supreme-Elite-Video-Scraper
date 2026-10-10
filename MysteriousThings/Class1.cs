using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MysteriousThings
{

    public class Class1
    {
        public Class1() { }
        public void core(string Profile_str)
        { 
            using Stream video = Assembly.GetExecutingAssembly().GetManifestResourceStream("MysteriousThings.Video.Video.mp4")
                ?? throw new InvalidOperationException("未找到内嵌视频资源。");
            Console.Title = ("视频下载器");
            Console.WriteLine($"可自动下载来自{Profile_str}的视频");
            int a = 1;
            long num = 0;
            string desk1 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//获取桌面
            string file = Path.Combine(desk1, "download");
            Console.WriteLine($"请输入保存位置（留空使用默认位置：{file}）");
            string? downloadPath = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(downloadPath))
            {
                file = downloadPath.Trim();
            }

            string? input;
            do
            {


                Console.WriteLine("请输入下载个数");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    
                    Console.WriteLine("下载数不可为空！");
                    
                }
                else
                {
                    if (int.TryParse(input, out int input_int))
                    {
                        if (input_int > 0)
                        {
                            Directory.CreateDirectory(file);//创建桌面的文件夹
                            do
                            {

                                Console.WriteLine($"已下载{num + 1}个视频，保存至{file}");
                                string file1 = Path.Combine(file, $"Video{num}.mp4");
                                video.Position = 0;
                                using (FileStream output = File.Create(file1))
                                {
                                    video.CopyTo(output);
                                }
                                num++;
                                Thread.Sleep(Random.Shared.Next(1000, 3001));
                            } while (num < input_int);

                            Console.WriteLine("下载完毕！");
                            Console.WriteLine("按任意键退出...");
                            if (Console.IsInputRedirected)
                            {
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.ReadKey(true);
                            }
                            return;
                        }
                        else
                        {
                            Console.WriteLine("下载次数不能小于1！");
                        }

                    }
                    else
                    {
                        Console.WriteLine("请输入整数!");
                    }
                }
            }
            while (a>0);
           
           

        }
        
    }

}
