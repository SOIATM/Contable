Imports System.Text
Imports System.Security.Cryptography
Imports Telerik.WinControls
Public Class ResetPass
    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")

        If RadMessageBox.Show(Me, "Deseas Activar la contraseña del Usuario: " & Me.lstUsuarios.SelectText & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Me.TxtPass.Text = Me.TxtPass2.Text Then
                Dim enc As New UTF8Encoding
                Dim data() As Byte = enc.GetBytes(Me.TxtPass.Text)
                Dim result() As Byte
                Dim sha As New SHA1CryptoServiceProvider
                result = sha.ComputeHash(data)
                Dim sb As New StringBuilder
                Dim max As Int32 = result.Length
                For i As Integer = 0 To max - 1
                    If (result(i) < 16) Then
                        sb.Append("0")
                    End If
                    sb.Append(result(i).ToString("x"))
                Next

                Dim pas As String = sb.ToString().ToUpper()
                Dim sql = "UPDATE dbo.Usuarios SET Contraseña = '" & pas & "'WHERE Id_Matricula = " & Me.lstUsuarios.SelectItem & " "
                If Eventos.Comando_sql(sql) > 0 Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    Dim Ms As DialogResult = RadMessageBox.Show(Me, "Contraseña Activada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                    Me.Text = Ms.ToString()

                End If
                Me.Close()
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "Las contraseñas no Coinciden", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()

                Me.TxtPass.Text = ""
                Me.TxtPass2.Text = ""
            End If

        Else
            Exit Sub
        End If
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub ResetPass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.lstUsuarios.Cargar("SELECT id_matricula , Usuario from Usuarios where id_empresa = 1")
        Me.lstUsuarios.SelectText = ""
    End Sub

    Private Sub TxtPass_TextChanged(sender As Object, e As EventArgs) Handles TxtPass.TextChanged
        If Me.TxtPass.Text = Me.TxtPass2.Text Then
            Me.Cmd_Procesar.Enabled = True
        Else
            Me.Cmd_Procesar.Enabled = False
        End If
    End Sub

    Private Sub TxtPass2_TextChanged(sender As Object, e As EventArgs) Handles TxtPass2.TextChanged
        If Me.TxtPass.Text = Me.TxtPass2.Text Then
            Me.Cmd_Procesar.Enabled = True
        Else
            Me.Cmd_Procesar.Enabled = False
        End If
    End Sub
End Class
