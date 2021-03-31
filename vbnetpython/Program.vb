Imports System
Imports System.IO
Imports System.Text

Module Program
    Sub Main(args As String())
        'This is how a comment is written. - https://stackoverflow.com/questions/13477958/in-visual-basic-how-do-you-create-a-block-comment
        Console.WriteLine("Hello World!")

        'This is where the folders and files will be
        Dim path As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\"



        'read from a file - https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalltext?view=net-5.0
        Dim CSVpath As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\CSV_19891111.csv"

        If File.Exists(CSVpath) = True Then

            ' Open the file to read from.
            Dim readText As String = File.ReadAllText(CSVpath)
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

        'create the set of folders
        WriteToFile(info, path, filename:="config.txt")
        CreateFolder("CSVfiles")
        CreateFolder("csvFinalOutput")
        CreateFolder("OutputForXML")
        CreateFolder("FilesProcessed")
        CreateFolder("Logs")

        'get the current time
        Dim time As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        'ToString("d")
        'ToLongTimeString
        Console.WriteLine(time)
        'use the current time to create a unique name for the log file
        'It is not necessary to use a unique name while writing the program.
        Dim logInfo As Byte() = New UTF8Encoding(True).GetBytes("Information about the conversion From csv to xml")
        WriteToFile(logInfo, path + "Logs\", filename:="log.txt") 'time + "log.txt")




    End Sub

    'sub or function - https://stackoverflow.com/questions/10141708/what-is-the-difference-between-sub-and-function-in-vb6
    Public Sub WriteToFile(text, path, filename)


        'Dim filename As String = "config.txt"
        ' Create or overwrite the file.
        Dim fs As FileStream = File.Create(path + filename)
        fs.Write(text, 0, text.Length)
        fs.Close()


    End Sub

    Public Sub CreateFolder(ByRef folderName As String)
        'create a folder - https://docs.microsoft.com/en-us/dotnet/visual-basic/developing-apps/programming/drives-directories-files/how-to-create-a-directory
        '- https://stackoverflow.com/questions/85996/how-do-i-create-a-folder-in-vb-if-it-doesnt-exist
        Directory.CreateDirectory(
  "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\" + folderName)
    End Sub

    Function ConcatenateCSV(ByVal path As String) As String
        ' local variable declaration */
        Dim result As String = "test"
        'this might not be necessary
        'For Each foundFile As String In File(path)
        '   My.Computer.FileSystem.SpecialDirectories.MyDocuments)

        '   listBox1.Items.Add(foundFile)
        'Next
        Return result
        'Get the Collection of Files in a Directory - https://docs.microsoft.com/en-us/dotnet/visual-basic/developing-apps/programming/drives-directories-files/how-to-get-the-collection-of-files-in-a-directory
    End Function

End Module
