Imports System
Imports System.IO
Imports System.Text

Module Program
    Dim path As String
    Sub Main(args As String())
        'This is how a comment is written. - https://stackoverflow.com/questions/13477958/in-visual-basic-how-do-you-create-a-block-comment
        Console.WriteLine("The program has started.")

        'You need to know the directory path where the program is running to be able to get other files near it.  Without the ability to get the program's directory path, the directory path needs to be coded as a string in the program.
        'Get the directory path where the program is running - vb.net get path of application - https://stackoverflow.com/questions/2216141/get-program-path-in-vb-net
        'These did not work from that page.
        'Dim appPath As String = Application.StartupPath()
        'Dim exePath As String = Application.ExecutablePath()
        'Dim appPath As String = My.Application.Info.DirectoryPath
        'System.Reflection.Assembly.GetExecutingAssembly().Location
        'Dim CurDir As String = My.Application.Info.DirectoryPath
        'This line worked.  It gets the directory path of the .exe file.
        Dim strPath As String = AppDomain.CurrentDomain.BaseDirectory
        'The way the program is set up now, the .exe file is not in the same folder as the .csv file or the .ini file.
        'When the program is finished, it may need to be set up so that only the .exe file and .ini file are given without any of the other folders or files that Visual Studio sets up.
        'The program's path might not be where the .csv files are.  Even if the .csv files will be in another folder that will be defined by the user in the .ini file, the path is still needed to access the .ini file.
        Console.WriteLine("The .exe path is " & strPath)
        'vb.net to executable - https://stackoverflow.com/questions/17130995/how-to-make-a-vb-net-file-program-to-standalone-exe

        'This is where the folders and files will be
        'Path should be taken from config file
        'Dim path As String
        Dim configContent As String
        Dim configPath As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\config.ini"
        If File.Exists(configPath) = True Then
            ' Open the file to read from.
            configContent = File.ReadAllText(configPath)
        Else
            configContent = ""
            path = ""
            Console.WriteLine("no config.ini file found")
        End If
        'Dim path As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\"
        Dim configLine As String() = configContent.Split(New String() {Environment.NewLine}, '{ "\r\n", "\r", "\n" }
                                       StringSplitOptions.None)
        path = configLine(0)
        Dim csvFilename = configLine(1)


        Dim CSVpath As String = path & "CSVfiles\" + csvFilename
        'Console.WriteLine(ReadFile(CSVpath))


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
        'These only create folders if the folders do not exist - maybe it should be expected that the folders already exist
        CreateFolder("CSVfiles")
        CreateFolder("csvFinalOutput")
        CreateFolder("OutputForXML")
        CreateFolder("FilesProcessed")
        CreateFolder("Logs")

        'get the current time
        Dim time As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        'ToString("d")
        'ToLongTimeString
        'Console.WriteLine(time)
        'use the current time to create a unique name for the log file
        'It is not necessary to use a unique name while writing the program.
        Dim logInfo As Byte() = New UTF8Encoding(True).GetBytes("Information about the conversion From csv to xml" & " - .exe is at -" & strPath) ' - the program was run at " & Now.ToString("d") & " " & Now.ToLongTimeString)
        WriteToFile(logInfo, path + "Logs\", filename:="log.txt") 'time + "log.txt")

        'Console.WriteLine(CreateXML)
        'convert string to bytes for WriteToFile
        Dim xmlString As Byte() = New UTF8Encoding(True).GetBytes(CreateXML)
        'This is where the xml is written to a file
        WriteToFile(xmlString, path + "OutputForXML\", filename:="FinalXML_" & ".xml") ' & time & ".xml")
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
  path + folderName)
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

        'I noticed that a comma in a cell causes the rest of the line to be in the wrong place. There needs to be a way to fix that.
        'Any time a comma is used in a cell, excel puts it in quotes.  I need to find some way of not counting the commas that are inside a set of quotes.
        'I could change any commas that are between two quotes to something else, and maybe change them back to commas after splitting the line at the commas.
        'Go through each line to check for quote marks.
        'Go through all the characters in the line to check if it's a quote mark.
        'Stop at the end of each line.
        'If there are 2 quote marks on a line, remove or replace all commas between them.
        'The commas can be put back after the split
        'https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/how-to-search-within-a-string
        For lineIndex As Integer = 0 To line.Length - 1
            For charIndex As Integer = 0 To line(lineIndex - 1)

            Next
        Next


        'count the number of commas
        Dim countColumn As Integer = CountChar(line(0), ",") + 1
        Console.WriteLine("There are " & countColumn & " columns in the file.")
        'split the line by the commas into columns
        'columnNames array
        Dim columnNames As String() = line(0).Split(New String() {","}, StringSplitOptions.None)

        'output the column names
        'For i As Integer = 0 To countColumn - 1
        'Console.WriteLine("Column " & i & " is " & columnNames(i) & ".")
        'Next

        Console.WriteLine("The number of lines of in the file is " & line.Length & ".")
        'Dim test As Integer() = {1, 5}
        'Console.WriteLine("The number for this test is " & test.Length) 'output is 2.
        'Create a 2d array for the data in the table
        Dim fileData(countColumn, line.Length - 1) As String
        'go from the second line to the last line to get the data content as an array
        'array length in VB.NET is one more than the number of elements in the array - https://stackoverflow.com/questions/506207/size-of-array-in-visual-basic
        'maybe not - this will need to be reviewed - check line.length
        For y As Integer = 1 To line.Length - 1
            fileData(0, y) = ""

            'split the line into columns
            Dim columnData As String() = line(y).Split(New String() {","}, StringSplitOptions.None)
            'go From the first column to the last column in the line
            For x As Integer = 1 To countColumn

                'add the data to the 2d array
                fileData(x, y) = columnData(x - 1)
                'Console.WriteLine(fileData(x, y))


            Next
        Next

        'Find the column for the data outside the For loop
        Console.WriteLine("The uid column is at " & ColumnSearch("No", columnNames))
        Dim uidLocation As Integer = ColumnSearch("No", columnNames)
        Dim fullnameLocation As Integer = ColumnSearch("SDN Name", columnNames)
        Dim programLocation As Integer = ColumnSearch("Program", columnNames)
        Dim partytypeLocation As Integer = ColumnSearch("Type", columnNames)
        Dim countryLocation As Integer = ColumnSearch("Country", columnNames)
        Dim sexLocation As Integer = ColumnSearch("Sex", columnNames)
        Dim sexData As String
        Dim remarksLocation As Integer = ColumnSearch("Remarks", columnNames)
        Dim entitytypeLocation As Integer = ColumnSearch("Type", columnNames)
        Dim listOfAkasLocation As Integer = ColumnSearch("Alias", columnNames)
        Dim addressLocation As Integer = ColumnSearch("Address", columnNames)
        Dim cityLocation As Integer = ColumnSearch("City", columnNames)
        'Dim countryLocation As Integer = ColumnSearch("Country", columnNames)
        Dim dobLocation As Integer = ColumnSearch("DOB", columnNames)
        Dim pobLocation As Integer = ColumnSearch("POB", columnNames)
        Dim pobData As String

        'add the info for each user line by line in xml format
        For y As Integer = 1 To line.Length - 1
            builder.Append("      <party>
        <info>" & vbCrLf)

            builder.Append("          <uid>" & fileData(ColumnSearch("No", columnNames), y) & "</uid>" & vbCrLf)
            builder.Append("          <fullname>" & fileData(ColumnSearch("SDN Name", columnNames), y) & "</fullname>" & vbCrLf)
            builder.Append("          <firstname></firstname>" & vbCrLf)
            builder.Append("          <middlename></middlename>" & vbCrLf)
            builder.Append("          <lastname></lastname>" & vbCrLf)
            builder.Append("          <title></title>" & vbCrLf)
            builder.Append("          <program>" & fileData(ColumnSearch("Program", columnNames), y) & "</program>" & vbCrLf)
            builder.Append("          <partytype>" & fileData(ColumnSearch("Type", columnNames), y) & "</partytype>" & vbCrLf)
            builder.Append("          <vesstype></vesstype>" & vbCrLf)
            builder.Append("          <tonnage></tonnage>" & vbCrLf)
            builder.Append("          <grt></grt>" & vbCrLf)
            'Date of Birth must be converted to mmddyyyy format.
            'Why is date of birth here when it is in another place in a lower line
            builder.Append("          <dob></dob>" & vbCrLf)
            'sex should default to "m" when it is not included in the csv file
            If fileData(sexLocation, y).Equals("") Then
                sexData = "m"
            Else
                sexData = fileData(sexLocation, y)
            End If
            builder.Append("          <sex>" & sexData & "</sex>" & vbCrLf)
            builder.Append("          <height></height>" & vbCrLf)
            builder.Append("          <weight></weight>" & vbCrLf)
            builder.Append("          <build></build>" & vbCrLf)
            builder.Append("          <eyes></eyes>" & vbCrLf)
            builder.Append("          <hair></hair>" & vbCrLf)
            builder.Append("          <complexion></complexion>" & vbCrLf)
            builder.Append("          <race></race>" & vbCrLf)
            'country is in another place in a lower line at address
            builder.Append("          <country></country>" & vbCrLf)
            builder.Append("          <remarks>" & fileData(remarksLocation, y) & "</remarks>" & vbCrLf)
            builder.Append("          <entitytype>" & fileData(entitytypeLocation, y) & "</entitytype>" & vbCrLf)
            builder.Append("          <listtype></listtype>" & vbCrLf)
            builder.Append("          <callsign></callsign>" & vbCrLf)
            builder.Append("        </info>" & vbCrLf)
            builder.Append("        <akas>" & vbCrLf)
            'There can be many akas, or no akas.  If there are zero akas, then a blank aka space must be shown in xml.
            Dim listOfAkas As String() = fileData(ColumnSearch("Alias", columnNames), y).Split(New String() {"@"}, StringSplitOptions.None)
            'builder.Append("The list of akas has a length of " & listOfAkas.Length & ".")
            'The array of akas has a length of 1 even if there are no akas.  An array with a single null space is an array of 1.
            For i As Integer = 0 To listOfAkas.Length - 1
                'If there are no akas, then the length is 1.  Loop (For 0 To 0) goes through the loop once.

                builder.Append("        <aka>" & vbCrLf)
                builder.Append("            <alttype>a.k.a.</alttype>" & vbCrLf)
                builder.Append("            <altname>" & listOfAkas(i) & "</altname>" & vbCrLf)
                builder.Append("            <firstname></firstname>" & vbCrLf)
                builder.Append("            <middlename></middlename>" & vbCrLf)
                builder.Append("            <lastname></lastname>" & vbCrLf)
                builder.Append("            <remarks></remarks>" & vbCrLf)
                builder.Append("        </aka>" & vbCrLf)
            Next
            builder.Append("        </akas>" & vbCrLf)
            builder.Append("        <addresses>" & vbCrLf)
            'The example xml showed 4 address fields, even when none are used.  Why not handle addresses the same as akas?
            builder.Append("          <address>" & vbCrLf)
            builder.Append("            <addr1>" & fileData(addressLocation, y) & "</addr1>" & vbCrLf)
            builder.Append("            <addr2></addr2>" & vbCrLf)
            builder.Append("            <addr3></addr3>" & vbCrLf)
            builder.Append("            <addr4></addr4>" & vbCrLf)
            builder.Append("            <city>" & fileData(cityLocation, y) & "</city>" & vbCrLf)
            builder.Append("            <state></state>" & vbCrLf)
            builder.Append("            <postalcode></postalcode>" & vbCrLf)
            builder.Append("            <country>" & fileData(countryLocation, y) & "</country>" & vbCrLf)
            builder.Append("            <remarks></remarks>" & vbCrLf)
            builder.Append("          </address>" & vbCrLf)
            builder.Append("        </addresses>" & vbCrLf)
            builder.Append("        <dobs>" & vbCrLf)
            builder.Append("          <dob>" & fileData(dobLocation, y) & "</dob>" & vbCrLf)
            builder.Append("        </dobs>" & vbCrLf)
            'POB - place of birth - should be in notes
            builder.Append("        <notes>" & vbCrLf)
            builder.Append("          <note type="""" value=""""/>" & vbCrLf)
            'If POB is blank, it should be NA.
            If fileData(pobLocation, y).Equals("") Then
                pobData = "NA"
            Else
                pobData = fileData(pobLocation, y)
            End If
            builder.Append("          <note type=""POB"" value=""" & pobData & """/>" & vbCrLf)
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

    'columns are not always in the same order, so a function is needed to find the position of each column
    Public Function ColumnSearch(ByVal columnName As String, ByVal NameList As String()) As Integer

        For i As Integer = 1 To NameList.Length - 1
            If columnName.Equals(NameList(i)) Then
                Return i + 1
            End If
        Next
        'if it is not found, there should be a number that gives an empty response
        Return 0
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
