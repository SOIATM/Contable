Imports Telerik.WinControls
Public Class Letras_Contabilizacion
    Dim activo As Boolean
    Private Sub Letras_Contabilizacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Me.lstCliente_Enters()
        activo = False
    End Sub

    Private Sub Tabla_detalleLetras_cmd_Editar(clave As String) Handles Tabla_detalleLetras.Cmd_Editar

    End Sub

    Private Sub Tabla_detalleLetras_cmd_eliminar(clave As String) Handles Tabla_detalleLetras.Cmd_eliminar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar el La Contabilizacion: " & Me.Tabla_detalleLetras.Registro_columna(2) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Letras_Contabilidad where Id_Empresa= " & Me.lstCliente.SelectItem & " and Id_Contabilidad=" & clave) > 0 Then
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Eventos.Insertar_usuariol("Letras_Contabilidad_D", "Delete from dbo.Letras_Contabilidad where Id_Empresa= " & Me.lstCliente.SelectItem & " and Id_Contabilidad= " & clave & "")
            Else
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub Tabla_detalleLetras_cmd_Nuevo() Handles Tabla_detalleLetras.Cmd_Nuevo
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        'If Me.lstCliente.SelectText <> "" Then
        '    CargaMasivaLetras.Cliente = Me.lstCliente.SelectItem
        '    Eventos.Abrir_form(CargaMasivaLetras)
        'Else
        '    RadMessageBox.Show("Debes Seleccionar una Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        'End If

    End Sub

    Private Sub Tabla_detalleLetras_Cerrar() Handles Tabla_detalleLetras.Cerrar
        Me.Close()
    End Sub

    Private Sub lstCliente_Enters() Handles lstCliente.Enters
        Me.Tabla_detalleLetras.SqlSelect = "SELECT  	Id_Contabilidad,	Clave,	Descripcion FROM dbo.Letras_Contabilidad where Letras_Contabilidad.Id_Empresa= " & Me.lstCliente.SelectItem & ""
        Me.Tabla_detalleLetras.Cargar()
        Me.Tabla_detalleLetras.Tabla.Columns(0).Visible = False
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Contabilización")
    'End Sub
End Class