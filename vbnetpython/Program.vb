Imports System
Imports System.IO
Imports System.Text

Module Program
    Dim path As String
    Sub Main(args As String())
        'This is how a comment is written. - https://stackoverflow.com/questions/13477958/in-visual-basic-how-do-you-create-a-block-comment
        Console.WriteLine("Hello World!")

        'This is where the folders and files will be
        'Path should be taken from config file
        'Dim path As String
        Dim configPath As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\config.txt"
        If File.Exists(configPath) = True Then

            ' Open the file to read from.
            path = File.ReadAllText(configPath)
        Else
            path = ""
            Console.WriteLine("no config.txt file found")
        End If
        'Dim path As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\"




        Dim CSVpath As String = path & "CSVfiles\" + "CSV_19891111.csv"
        Console.WriteLine(ReadFile(CSVpath))


        'write to a file -
        'https://docs.microsoft.com/en-us/dotnet/visual-basic/developing-apps/programming/drives-directories-files/how-to-create-a-file
        'Dim path As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\vbnetpython\test.txt"

        ' Create or overwrite the file.
        'Dim fs As FileStream = File.Create(path)

        ' Add text to the file.
        'newline - https://stackoverflow.com/questions/5152042/how-to-use-n-new-line-in-vb-msgbox & vbcrlf &
        'https://www.tutorialspoint.com/vb.net/vb.net_character_escapes.htm \n
        'https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-put-quotation-marks-in-a-string-windows-forms?view=netframeworkdesktop-4.8 ""
        'Dim info As Byte() = New UTF8Encoding(True).GetBytes(
        '   "{""read_csv_from"" : ""C:    \\.....\\MUFG\\CSVfiles\\"",
        '""write_final_csv_to"" : ""C\\.....\\MUFG\\OutputForXML\\"",
        '""processedCsvs"" : ""C\\.....\\MUFG\\FilesProcessed\\"",
        '""logs"":""C\\.....\\MUFG\\Logs\\"" }")
        'fs.Write(info, 0, info.Length)
        'fs.Close()

        'create the set of folders
        'WriteToFile(info, path, filename:="config.txt")
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



        Console.WriteLine(CreateXML)
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


    'StringBuilder - https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/how-to-create-strings-using-a-stringbuilder
    'read line by line - https://docs.microsoft.com/en-us/dotnet/visual-basic/developing-apps/programming/drives-directories-files/how-to-read-text-from-files-with-a-streamreader
    Private Function CreateXML() As String
        Dim builder As New System.Text.StringBuilder

        Dim CSVpath As String = path & "CSVfiles\" + "CSV_19891111.csv"
        builder.Append("<?xml version=""1.0"" encoding=""utf-8""?>
    <parties>" & vbCrLf)

        Dim fileContent As String = ReadFile(CSVpath)
        'split - https://stackoverflow.com/questions/14795943/how-to-split-new-line-in-string-in-vb-net
        'split the file into lines
        Dim line As String() = fileContent.Split(New String() {Environment.NewLine}, '{ "\r\n", "\r", "\n" }
                                       StringSplitOptions.None)
        'count the number of commas
        Dim countColumn As Integer = CountChar(line(0), ",") + 1
        Console.WriteLine("There are " & CountColumn & " columns in the row.")
        'split the line by the commas into columns
        'columnName array
        Dim columnName As String() = line(0).Split(New String() {","}, StringSplitOptions.None)
        Console.WriteLine("The second column name is " & columnName(1) & ".")
        'output the column names
        For i As Integer = 0 To countColumn - 1
            Console.WriteLine("Column " & i & " is " & columnName(i) & ".")
        Next

        'Create a 2d array for the data in the table
        Dim fileData(countColumn, line.Length - 1) As String
        'go from the second line to the last line to get the data content as an array
        'array length in VB.NET is one more than the number of elements in the array - https://stackoverflow.com/questions/506207/size-of-array-in-visual-basic
        For y As Integer = 1 To line.Length - 1
            Console.WriteLine(y)

            'split the line into columns
            Dim columnData As String() = line(y).Split(New String() {","}, StringSplitOptions.None)
            'go From the first column to the last column in the line
            For x As Integer = 1 To countColumn


                fileData(x, y) = "something"
                Console.WriteLine(fileData(x, y))


            Next
        Next




        For i As Integer = 1 To 10
            builder.Append("Step " & i & vbCrLf)
        Next
        Return builder.ToString
    End Function

    Public Function CountChar(ByVal value As String, ByVal ch As Char) As Integer
        'count how many commas are in the line
        'https://stackoverflow.com/questions/5193893/count-specific-character-occurrences-in-a-string
        Dim count As Integer = 0
        For Each c As Char In value
            If c = ch Then
                count += 1
            End If
        Next
        Return count
    End Function

    'Read a text file
    Private Function ReadFile(filePath) As String
        'read from a file - https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalltext?view=net-5.0
        If File.Exists(filePath) = True Then
            ' Open the file to read from.
            Dim readText As String = File.ReadAllText(filePath)
            'Console.WriteLine(readText)
            Return readText
        Else
            Console.WriteLine("file not found")
            Return "file not found"
        End If
    End Function
End Module
