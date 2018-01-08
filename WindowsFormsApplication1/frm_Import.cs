using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frm_Import : Form
    {

        public enum ImportType {
            Model,
            HeightMap,
            File
        }

        public ImportType ContentType { get; set; } = ImportType.Model;

        protected string Folder => $"{Enum.GetName(typeof(ImportType), ContentType)}s";

        protected string Filter {
            get {
                switch (ContentType) {
                    case ImportType.Model: {
                        return "FBX and PNG files|*.png;*.fbx";
                    }
                    case ImportType.HeightMap: {
                        return "tif files (Alpha height)|*.tif";
                    }
                    default:
                        return "Not implemented";
                }
            }
        }

        protected string Title
        {
            get
            {
                switch (ContentType)
                {
                    case ImportType.Model:
                    {
                        return "Please select a .FBX file and its texture map";
                    }
                    case ImportType.HeightMap:
                    {
                        return "Please select a HeightMap map";
                    }
                    default:
                        return "Not implemented";
                }
            }
        }


        public frm_Import()
        {
            InitializeComponent();
        }

        private void button_Import_Click(object sender, EventArgs e) {

            // Add selection filter
            openFileDialog1.Filter = Filter;
            openFileDialog1.Title = Title;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //create file header
                string fileContent =
@"#----------------------------- Global Properties ----------------------------#

/outputDir:bin
/intermediateDir:obj
/platform:Windows
/config:
/profile:HiDef
/compress:False

#-------------------------------- References --------------------------------#
";
                string outputDir = Path.Combine(Application.StartupPath, "SourceAssets");
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "SourceAssets"));
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, @"SourceAssets/bin"));

                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {
                    File.Delete(Path.Combine(outputDir, openFileDialog1.SafeFileNames[i]));
                    File.Copy(openFileDialog1.FileNames[i], Path.Combine(outputDir, openFileDialog1.SafeFileNames[i]));
                    string newEntry =
                    PrepareEntry(openFileDialog1.SafeFileNames[i], Path.GetExtension(openFileDialog1.SafeFileNames[i]));
                    fileContent += newEntry;
                }

                textBox_MGCBContent.Text = fileContent;
                string outputMGCBFilename = Path.Combine(Application.StartupPath, @"SourceAssets\ContentToBuild.mgcb");
                string fullPathToMGCBExecutable = @"C:/Program Files (x86)/MSBuild/MonoGame/v3.0/Tools/MGCB.exe";
                string platform = "/platform:Windows";

                //save the .MGCB file                
                File.WriteAllText(outputMGCBFilename, fileContent);

                //create batch file
                string outputBatchContent = "cmd /c \"\"" + fullPathToMGCBExecutable + "\" " + platform + " /@:\"" + outputMGCBFilename + "\"";
                string outputBatchFileLocation = Path.Combine(Application.StartupPath, @"SourceAssets\buildcontent.bat");
                File.WriteAllText(outputBatchFileLocation, outputBatchContent);

                //call batch file
                Process proc = new Process();
                proc.StartInfo.FileName = outputBatchFileLocation;
                proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(outputMGCBFilename);
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                //now we copy across the files to the target directory
                string[] fileList = Directory.GetFiles(Path.Combine(Application.StartupPath, @"SourceAssets\bin"));
                string targetDirectory = Path.Combine(Application.StartupPath, $@"Content\{Folder}");
                for (int i = 0; i < fileList.Length; i++)
                {
                    string targetPlusFilename = Path.Combine(targetDirectory, Path.GetFileName(fileList[i]));
                    File.Copy(fileList[i], targetPlusFilename, true);
                }
                MessageBox.Show("Done");
            }
        }

        public string PrepareEntry(string fileName, string fileExtension)
        {
            string output = "#begin " + fileName;
            fileExtension = fileExtension.ToLower();
            switch (fileExtension)
            {
                case ".fbx":
                    {
                        output += Environment.NewLine +
@"/importer:FbxImporter
/processor:ModelProcessor
/processorParam:ColorKeyColor=0,0,0,0
/processorParam:ColorKeyEnabled=True
/processorParam:DefaultEffect=BasicEffect
/processorParam:GenerateMipmaps=True
/processorParam:GenerateTangentFrames=False
/processorParam:PremultiplyTextureAlpha=True
/processorParam:PremultiplyVertexColors=True
/processorParam:ResizeTexturesToPowerOfTwo=False
/processorParam:RotationX=0
/processorParam:RotationY=0
/processorParam:RotationZ=0
/processorParam:Scale=1
/processorParam:SwapWindingOrder=False
/processorParam:TextureFormat=DxtCompressed
/build:" + fileName;
                    }
                    break;
                case ".png":
                    {
                        output += Environment.NewLine +
@"/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:TextureFormat=Color
/build:" + fileName;
                    }
                    break;
                case ".tif":
                {
                    output += Environment.NewLine +
@"/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=False
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=False
/processorParam:ResizeToPowerOfTwo=False
/processorParam:TextureFormat=Color
/build:" + fileName;
                }
                    break;
                default:
                    {
                        //something else, skip it
                    }
                    break;
            }
            output += Environment.NewLine + Environment.NewLine;
            return output;
        }
    }
}
