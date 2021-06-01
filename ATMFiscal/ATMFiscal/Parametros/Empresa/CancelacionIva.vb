Imports Telerik.WinControls
Public Class CancelacionIva
    Dim activo As Boolean
    Private Sub CancelacionIva_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub Tabla_detallePolizasSat_cmd_Nuevo() Handles Tabla_detalleCancelaciones.Cmd_Nuevo
        If Me.lstCliente.SelectText <> "" Then
            MasivoCancelacionesIVA.Cliente = Me.lstCliente.SelectItem
            Eventos.Abrir_form(MasivoCancelacionesIVA)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Debes seleccionar una empresa primero...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub lstCliente_Enters() Handles lstCliente.Enters
        If Me.lstCliente.SelectText <> "" Then
            Me.Tabla_detalleCancelaciones.SqlSelect = "SELECT 	Id_Cancelacion,	Catalogo_de_Cuentas.Cuenta, Catalogo_de_Cuentas.Descripcion,CancelacionesIVA.Abono, CancelacionesIVA.Id_Empresa FROM dbo.CancelacionesIVA INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = CancelacionesIVA.Id_cat_Cuentas where CancelacionesIVA.Id_Empresa= " & Me.lstCliente.SelectItem & " order by Cuenta"
            Me.Tabla_detalleCancelaciones.Cargar()
            Me.Tabla_detalleCancelaciones.Tabla.Columns(0).Visible = False
            Me.Tabla_detalleCancelaciones.Tabla.Columns(4).Visible = False
            Me.Tabla_detalleCancelaciones.CmdEditar.Enabled = False
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

        End If
    End Sub

    Private Sub Tabla_detalleCancelaciones_cmd_eliminar(clave As String) Handles Tabla_detalleCancelaciones.Cmd_eliminar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar el Tipo: " & Me.Tabla_detalleCancelaciones.Registro_columna(2) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.CancelacionesIVA where Id_Cancelacion=" & clave) > 0 Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Eventos.Insertar_usuariol("CancelacionesIVA", "Delete from dbo.CancelacionesIVA where Id_Cancelacion= " & clave & "")
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub Tabla_detalleCancelaciones_Cerrar() Handles Tabla_detalleCancelaciones.Cerrar
        Me.Close()
    End Sub


End Class
