Imports System
Imports System.IO
Imports System.Text

Module Program
    Sub Main(args As String())
        'This is how a comment is written. - https://stackoverflow.com/questions/13477958/in-visual-basic-how-do-you-create-a-block-comment
        Console.WriteLine("Hello World!")

        'read from a file - https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalltext?view=net-5.0
        Dim path As String = "C:\Users\BenjaminScalera\Documents\GitHub\VBnet-python\vbnetpython\test.txt"

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
        'Dim info As Byte() = New UTF8Encoding(True).GetBytes("This is some text in the file.")
        'fs.Write(info, 0, info.Length)
        'fs.Close()


    End Sub
End Module
