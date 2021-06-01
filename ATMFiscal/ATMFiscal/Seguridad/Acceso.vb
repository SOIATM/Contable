Imports System.ComponentModel
Imports System.Net.FtpWebRequest
Imports System.Net
Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports Telerik.WinControls
Public Class Acceso
    Public contraseñas As Integer
    Dim Intentos As Integer = 0
    Dim Caducidad As Date = "26/02/2021"
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click


        'If Caducidad = Today Then
        '    RadMessageBox.SetThemeName("MaterialBlueGrey")
        '    Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permisos para utilizar este ERP el periodo de licencia caduco ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        '    Me.Text = Ms.ToString()

        '    Application.Exit()
        'End If
        If Not UltimaVersion() Then

            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If RadMessageBox.Show(Me, "Estas usando una version diferente a la de la Base de Datos, deseas descargar el paquete de actualización ahora?", "Proyecto Contable", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Try
                    Process.Start("\\" & Trim(My.Forms.Inicio.txtServerDB.Text) & "\Soporte\ActualizacionesProyectoContable\Contable" & Eventos.Obtener_DS("select valor from parametros where parametro='Version'").Tables(0).Rows(0)(0).ToString & ".exe")
                    End
                Catch ex As Exception
                    End
                End Try
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "No puedes continuar hasta que tengas la version correcta...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()
                End
            End If
        End If
        If Me.TxtUsuario.Text <> "" Then

            If Me.TxtUsuario.Text.Contains("'") Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "Deteccion de Codigo Malicioso enviando notificacion", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()

                Application.Exit()
            End If
            If Me.TxtPass.Text <> "" Then

                If Me.TxtPass.Text.Contains("'") Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    Dim Ms As DialogResult = RadMessageBox.Show(Me, "Deteccion de Codigo Malicioso enviando notificacion", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    Me.Text = Ms.ToString()
                    Application.Exit()
                End If

            End If
        End If


        Dim ds As DataSet = Obtener_DS("Select * from Usuarios where Usuario='" & Me.TxtUsuario.Text & "'")
        If ds.Tables(0).Rows.Count = 1 Then


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
            If ds.Tables(0).Rows(0)("Contraseña") = pas Then
                'checar version de sw
                If Not permisos(Me.TxtUsuario.Text) Then
                    MessageBox.Show("No tienes permisos para utilizar la App " & ds.Tables(0).Rows(0)(2), Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    If Eventos.ObtenerValorDB("Personal", "sexo", " ID_matricula = " & ds.Tables(0).Rows(0)("ID_matricula") & "", True) = "M" Then
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        Dim Ms As DialogResult = RadMessageBox.Show(Me, "Bienvenido " & ds.Tables(0).Rows(0)("Usuario"), Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Me.Text = Ms.ToString()

                        ' MessageBox.Show("Bienvenido " & ds.Tables(0).Rows(0)("Usuario"), Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' Eventos.Hablar_sistema("Bienvenido " & ds.Tables(0).Rows(0)("Usuario"))
                    Else
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        Dim Ms As DialogResult = RadMessageBox.Show(Me, "Bienvenida " & ds.Tables(0).Rows(0)("Usuario"), Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Me.Text = Ms.ToString()
                        'Eventos.Hablar_sistema("Bienvenida " & ds.Tables(0).Rows(0)("Usuario"))
                    End If
                    Inicio.MenuInicio.CheckOnClick = True
                    Inicio.Txtestado.Text = "Conectado"
                    Inicio.LblUsuario.Text = Me.TxtUsuario.Text
                    Inicio.TxtEmpresa.Text = IIf(ds.Tables(0).Rows(0)("ID_empresa") = 1, "Autotransportación Mexicana ATM", "")
                    Inicio.MenuModulos.Enabled = True
                    Inicio.MenuVentanas.Enabled = True
                    Inicio.Clt = ds.Tables(0).Rows(0)("ID_empresa")
                    'Parametros para activar los privilegios master
                    If ds.Tables(0).Rows(0)("P_Master") = "M" Then
                        Inicio.MenuUsuarios.Enabled = True
                        Inicio.MenuEmpresa.Enabled = True

                    Else
                        Inicio.MenuUsuarios.Enabled = False
                        Inicio.MenuEmpresa.Enabled = False
                    End If
                    Inicio.Menuconta.Enabled = IIf(ds.Tables(0).Rows(0)(10) = "N", False, True)


                    Me.Close()

                End If
            Else
                Me.TxtPass.Clear()
                If Intentos >= 4 Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    Dim Ms As DialogResult = RadMessageBox.Show(Me, "Intentos de acceso excedidos el sistema se cerrara por seguridad y se notificara al administrador del sistema", "Proyecto Contable", MessageBoxButtons.OK, RadMessageIcon.Error)
                    Me.Text = Ms.ToString()

                    Application.Exit()
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    Dim Ms As DialogResult = RadMessageBox.Show(Me, "Contraseña incorrecta...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    Me.Text = Ms.ToString()

                    Intentos += 1
                End If
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "El usuario no existe.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Me.TxtPass.Clear()
            Me.TxtUsuario.Clear()
        End If

    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
    Private Function permisos(ByVal usuario As String) As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("Select P_Acceso from usuarios where Usuario='" & usuario & "'")
        If ds.Tables(0).Rows(0)(0) = "M" Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function UltimaVersion() As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("select * from parametros where parametro='Version'")
        If ds.Tables(0).Rows(0)("Valor") = Eventos.versionDB Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub Acceso_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
