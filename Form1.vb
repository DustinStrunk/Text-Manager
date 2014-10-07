



Imports System.Environment
Imports System.IO
Imports System.Runtime.InteropServices

Public Class Form1
    'Declarations 

    Dim appData = GetFolderPath(SpecialFolder.ApplicationData)
    Dim CSM = "\CS_Manager\"
    Dim counter As Integer = 0
    Dim treeParentText As String
    Dim parentnode As String
    Dim test As Boolean





    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs)
        Label14.Show()
        Button9.Show()
        TextBox5.Show()

    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Button12.Hide()
        Button7.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Hide()
        Label13.Hide()
        Button8.Hide()
        TreeView1.Show()
        TextBox2.Hide()
        Label14.Hide()
        Button9.Hide()
        TextBox5.Hide()

        ' see if the app data path exists
        If Directory.Exists(appData + "\CS_Manager\") Then


        Else
            Directory.CreateDirectory(appData + "\CS_Manager\")




        End If
        fillTree()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)


        If TextBox1.Text.Length = 0 Or TextBox1.Text.Length > 20 Or TextBox1.Text.Contains(".") Then
            MessageBox.Show("Please note, you must assign of value of 1 - 20 characters and they cann't conatin a backslash (\) or a dot (.)")
        Else
            For Each p In Directory.GetDirectories(appData + CSM)
                If TextBox1.Text = p.Substring(p.LastIndexOf("\") + 1) Then
                    MsgBox("There is already a directory named that!")
                    Dim counter = 0

                Else
                    Directory.CreateDirectory(appData + CSM + TextBox1.Text)
                End If
            Next


            fillTree()



        End If

    End Sub



    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
        Directory.Delete(appData + CSM + TreeView1.SelectedNode.Text)
        TreeView1.Nodes.Remove(TreeView1.SelectedNode)
    End Sub

    Private Sub fillTree()
        counter = 0
        TreeView1.Nodes.Clear()
        TreeView1.BeginUpdate()


        For Each f In Directory.GetDirectories(appData + CSM)

            TreeView1.Nodes.Add(f.Substring(f.LastIndexOf("\") + 1))




        Next
        Dim folders As String() = Directory.GetDirectories(appData + CSM)
        For Each q In folders
            For Each r In Directory.GetFiles(q)
                Dim s As Integer = r.LastIndexOf("\") + 1
                Dim p As Integer = r.LastIndexOf(".")
                Dim z As Integer = p - s
                TreeView1.Nodes(counter).Nodes.Add(r.Substring(s, z))
            Next
            counter = counter + 1
        Next
        counter = 0
        TreeView1.EndUpdate()




    End Sub


    ''''''
  
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        Dim treeViewText = TreeView1.SelectedNode.Text
        TextBox2.Show()

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click

        Dim sw As StreamWriter
        sw = New StreamWriter(appData + CSM + "\" + Label7.Text + "\" + TextBox3.Text + ".txt", False)
        sw.Write(TextBox2.Text)
        sw.Close()
        TextBox2.Clear()
        TextBox2.Hide()
        fillTree()

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs)
        Label13.Show()
        TextBox4.Show()
        Button8.Show()
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click


        If Label7.Text = "Select a Directory" Then
            MsgBox("Please select a directory!")
        Else
            My.Computer.FileSystem.RenameDirectory(appData + CSM + "\" + Label7.Text, TextBox4.Text)

            fillTree()
        End If
        Button8.Hide()
        Label13.Hide()
        TextBox4.Hide()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs)
        If test = False Then
            MsgBox("Please select a speech, not a directory!")
        Else
            File.Delete(appData + CSM + parentnode + "\" + TreeView1.SelectedNode.Text + ".txt")

            fillTree()
        End If

    End Sub

    Private Sub treeView1_AfterSelect(sender As Object, _
  e As TreeViewEventArgs) Handles TreeView1.AfterSelect 
        If (e.Node.Parent IsNot Nothing) Then
            If (e.Node.Parent.GetType() Is GetType(TreeNode)) Then
                parentnode = e.Node.Parent.Text
                treeParentText = e.Node.Parent.Text
                Label10.Text = e.Node.Text
                Label7.Text = e.Node.Parent.Text
                test = True
                Label4.Text = File.GetCreationTime(appData + CSM + e.Node.Parent.Text + "\" + e.Node.Text + ".txt")
                Dim nodeText = e.Node.Text
            End If
        Else
            Label8.Text = Directory.GetCreationTime(appData + CSM + e.Node.Text)
            Label7.Text = e.Node.Text
            test = False

        End If
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click

        My.Computer.FileSystem.RenameFile(appData + CSM + parentnode + "\" + Label10.Text + ".txt", TextBox5.Text + ".txt")

        fillTree()
    End Sub

   
    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        If test = False Then
            MsgBox("Please select a directory!")
        Else
            My.Computer.Clipboard.Clear()
            Dim path As String = appData + CSM + Label7.Text + "\" + Label10.Text + ".txt"
            Dim sr As StreamReader = New StreamReader(path)
            My.Computer.Clipboard.SetText(sr.ReadToEnd)
            sr.Close()
        End If
    End Sub

    Private Sub Button2_Click_1(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        TextBox3.Enabled = True : Button7.Enabled = True : TextBox2.Show()
    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Directory.Exists(appData + CSM + TextBox1.Text) = True Then
            MsgBox("Sorry that is already a directory!")
        End If
        Directory.CreateDirectory(appData + CSM + TextBox1.Text)
        fillTree()
    End Sub

    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs) Handles Button3.Click

        Try
            Directory.Delete(appData + CSM + Label7.Text, True)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        fillTree()
    End Sub

    Private Sub Button5_Click_1(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Try
            File.Delete(appData + CSM + Label7.Text + "\" + Label10.Text + ".txt")
        Catch ex As Exception

        End Try
        fillTree()
    End Sub

    Private Sub Button6_Click_1(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Button9.Show()
        Label14.Show()
        TextBox5.Show()
    End Sub

    Private Sub Button4_Click_1(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If test = False Then
            Button8.Show()
            Label13.Show()
            TextBox4.Show()
        Else
            MsgBox("Please select a directory!")
        End If
    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Button12.Show()
        Dim rd As String = appData + CSM + "\" + Label7.Text + "\" + Label10.Text + ".txt"
        Dim sr As StreamReader
        sr = New StreamReader(rd)
        TextBox2.Show()
        TextBox2.Text = sr.ReadToEnd
        sr.Close()
    End Sub

    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click

        File.Delete(appData + CSM + "\" + Label7.Text + "\" + Label10.Text + ".txt")
        Dim sw As StreamWriter
        sw = New StreamWriter(appData + CSM + "\" + Label7.Text + "\" + Label10.Text + ".txt", False)
        sw.Write(TextBox2.Text)
        sw.Close()
        TextBox2.Clear()
        TextBox2.Hide()
        TreeView1.Show()
        fillTree()



    End Sub
End Class
