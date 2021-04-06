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



        'Console.WriteLine(CreateXML)
        Dim xmlString As Byte() = New UTF8Encoding(True).GetBytes(CreateXML)
        'This is where the xml is written to a file
        WriteToFile(xmlString, path + "OutputForXML\", filename:="log" & time & ".txt")
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

        Console.WriteLine("The number of lines of data is " & line.Length)
        Dim test As Integer() = {1, 5}
        Console.WriteLine("The number for this test is " & test.Length) 'output is 2.
        'Create a 2d array for the data in the table
        Dim fileData(countColumn, line.Length - 1) As String
        'go from the second line to the last line to get the data content as an array
        'array length in VB.NET is one more than the number of elements in the array - https://stackoverflow.com/questions/506207/size-of-array-in-visual-basic
        'maybe not - this will need to be reviewed - check line.length
        For y As Integer = 1 To line.Length - 1
            Console.WriteLine(y)

            'split the line into columns
            Dim columnData As String() = line(y).Split(New String() {","}, StringSplitOptions.None)
            'go From the first column to the last column in the line
            For x As Integer = 1 To countColumn

                'add the data to the 2d array
                fileData(x, y) = columnData(x - 1)
                Console.WriteLine(fileData(x, y))


            Next
        Next

        'add the info for each user line by line in xml format
        For y As Integer = 1 To line.Length - 1
            builder.Append("      <party>
        <info>" & vbCrLf)
            builder.Append("          <uid>" & fileData(2, y) & "</uid>" & vbCrLf)
            builder.Append("          <fullname>" & fileData(3, y) & "</fullname>" & vbCrLf)
            builder.Append("          <firstname></firstname>" & vbCrLf)
            builder.Append("          <middlename></middlename>" & vbCrLf)
            builder.Append("          <lastname></lastname>" & vbCrLf)
            builder.Append("          <title></title>" & vbCrLf)
            builder.Append("          <program>" & fileData(10, y) & "</program>" & vbCrLf)
            builder.Append("          <partytype>" & fileData(22, y) & "</partytype>" & vbCrLf)
            builder.Append("          <vesstype></vesstype>" & vbCrLf)
            builder.Append("          <tonnage></tonnage>" & vbCrLf)
            builder.Append("          <grt></grt>" & vbCrLf)
            builder.Append("          <dob></dob>" & vbCrLf)
            builder.Append("          <sex></sex>" & vbCrLf)
            builder.Append("          <height></height>" & vbCrLf)
            builder.Append("          <weight></weight>" & vbCrLf)
            builder.Append("          <build></build>" & vbCrLf)
            builder.Append("          <eyes></eyes>" & vbCrLf)
            builder.Append("          <hair></hair>" & vbCrLf)
            builder.Append("          <complexion></complexion>" & vbCrLf)
            builder.Append("          <race></race>" & vbCrLf)
            builder.Append("          <country></country>" & vbCrLf)
            builder.Append("          <remarks></remarks>" & vbCrLf)
            builder.Append("          <entitytype></entitytype>" & vbCrLf)
            builder.Append("          <listtype></listtype>" & vbCrLf)
            builder.Append("          <callsign></callsign>" & vbCrLf)
            builder.Append("        </info>" & vbCrLf)
            builder.Append("        <akas>" & vbCrLf)
            'There can be many akas, or no akas.  If there are zero akas, then a blank aka space must be shown in xml.
            Dim listOfAkas As String() = fileData(4, y).Split(New String() {"@"}, StringSplitOptions.None)
            'builder.Append("The list of akas has a length of " & listOfAkas.Length & ".")
            'The array of akas has a length of 1 even if there are no akas.  An array with a single null space is an array of 1.
            For i As Integer = 0 To listOfAkas.Length - 1
                'If there are no akas, then the length is 1.  Loop (For 0 To 0) goes through the loop once.

                builder.Append("        <aka>" & vbCrLf)
                builder.Append("            <alttype></alttype>" & vbCrLf)
                builder.Append("            <altname>" & listOfAkas(i) & "</altname>" & vbCrLf)
                builder.Append("            <firstname></firstname>" & vbCrLf)
                builder.Append("            <middlename></middlename>" & vbCrLf)
                builder.Append("            <lastname></lastname>" & vbCrLf)
                builder.Append("            <remarks></remarks>" & vbCrLf)
                builder.Append("        </aka>" & vbCrLf)
            Next
            builder.Append("        </akas>" & vbCrLf)
            builder.Append("        <addresses>" & vbCrLf)
            builder.Append("          <address>" & vbCrLf)
            builder.Append("            <addr1></addr1>" & vbCrLf)
            builder.Append("            <addr2></addr2>" & vbCrLf)
            builder.Append("            <addr3></addr3>" & vbCrLf)
            builder.Append("            <addr4></addr4>" & vbCrLf)
            builder.Append("            <city></city>" & vbCrLf)
            builder.Append("            <state></state>" & vbCrLf)
            builder.Append("            <postalcode></postalcode>" & vbCrLf)
            builder.Append("            <country></country>" & vbCrLf)
            builder.Append("            <remarks></remarks>" & vbCrLf)
            builder.Append("          </address>" & vbCrLf)
            builder.Append("        </addresses>" & vbCrLf)
            builder.Append("        <dobs>" & vbCrLf)
            builder.Append("          <dob>" & fileData(11, y) & "</dob>" & vbCrLf)
            builder.Append("        </dobs>" & vbCrLf)
            builder.Append("        <notes>" & vbCrLf)
            builder.Append("          <note type="""" value=""""/>" & vbCrLf)
            builder.Append("        </notes>" & vbCrLf)
            builder.Append("        <urls>" & vbCrLf)
            builder.Append("          <url>" & vbCrLf)
            builder.Append("            <address>https://www.google.com</address>" & vbCrLf)
            builder.Append("            <description>https://www.google.com</description>" & vbCrLf)
            builder.Append("          </url>" & vbCrLf)
            builder.Append("        </urls>" & vbCrLf)
            builder.Append("      </party>" & vbCrLf)
        Next
        builder.Append("
        </parties>" & vbCrLf)

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
