Imports System
Imports System.IO
Imports System.Text

Module Program
    Sub Main(args As String())
        'This is how a comment is written. - https://stackoverflow.com/questions/13477958/in-visual-basic-how-do-you-create-a-block-comment
        Console.WriteLine("Hello World!")

        'read from a file - https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalltext?view=net-5.0
        Dim path As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\vbnetpython\CSV_19891111.csv"

        If File.Exists(path) = True Then

            ' Open the file to read from.
            Dim readText As String = File.ReadAllText(path)
            Console.WriteLine(readText)
        End If

        'write to a file -
        'https://docs.microsoft.com/en-us/dotnet/visual-basic/developing-apps/programming/drives-directories-files/how-to-create-a-file
        'Dim path As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\vbnetpython\test.txt"

        ' Create or overwrite the file.
        'Dim fs As FileStream = File.Create(path)

        ' Add text to the file.
        Dim info As Byte() = New UTF8Encoding(True).GetBytes("This is some text in the file.")
        'fs.Write(info, 0, info.Length)
        'fs.Close()
        WriteToFile(info)
        CreateFolder("CSVfiles")
        CreateFolder("csvFinalOutput")
        CreateFolder("OutputForXML")
        CreateFolder("FilesProcessed")
        CreateFolder("Logs")



    End Sub

    'sub or function - https://stackoverflow.com/questions/10141708/what-is-the-difference-between-sub-and-function-in-vb6
    Public Sub WriteToFile(info2)


        Dim path As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\vbnetpython\test.txt"
        ' Create or overwrite the file.
        Dim fs As FileStream = File.Create(path)
        fs.Write(info2, 0, info2.Length)
        fs.Close()


    End Sub

    Public Sub CreateFolder(ByRef folderName As String)
        'create a folder - https://docs.microsoft.com/en-us/dotnet/visual-basic/developing-apps/programming/drives-directories-files/how-to-create-a-directory
        '- https://stackoverflow.com/questions/85996/how-do-i-create-a-folder-in-vb-if-it-doesnt-exist
        Directory.CreateDirectory(
  "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\vbnetpython\" + folderName)
    End Sub
End Module
