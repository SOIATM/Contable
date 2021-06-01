Imports Telerik.WinControls
Public Class ReporteXml
	Private Sub CmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
		Me.Close()
	End Sub

	Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
		If Me.Tabla.Rows.Count > 0 Then
			Me.Tabla.Columns.Clear()
		End If
	End Sub

	Private Sub ReporteXml_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.lstCliente.Cargar("SELECT Empresa.Id_Empresa, Empresa.Razon_Social as Razon , Usuarios.Usuario
                                FROM     Empresa INNER JOIN
                                Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                                Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                                Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                                Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario

                                WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%') order by Razon ")
        Me.lstCliente.SelectItem = 1
        Eventos.DiseñoTabla(Me.Tabla)
    End Sub

	Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
		If Me.Tabla.Rows.Count > 0 Then
			Me.Tabla.Columns.Clear()
		End If
		If TxtFiltro.Text <> "" Then
			BuscarTusa()
		End If
	End Sub
	Private Sub BuscarTusa()

		RadMessageBox.SetThemeName("MaterialBlueGrey")
		Dim ds As DataSet
		Dim SQL = " SELECT x.UUID ,x.Orden , CASE WHEN x.Id_PolizaS IS NULL THEN 'Sin Poliza ATM' WHEN x.Id_PolizaS IS NOT NULL THEN x.Id_PolizaS END AS [Poliza ATM],"
		SQL &= "  CASE  WHEN x.Id_Poliza_Tusa IS NULL THEN 'Sin Poliza en TUSA' WHEN x.Id_Poliza_Tusa IS NOT NULL THEN x.Id_Poliza_Tusa END AS  [Poliza Tusa] , CASE WHEN xs.Id_Registro_Xml IS NULL THEN 'No se ha Importado' "
		SQL &= " WHEN xs.Id_Registro_Xml IS not NULL THEN 'Importado' END AS Estatus "
		SQL &= " FROM XmlAuditados AS X "
		SQL &= " LEFT OUTER JOIN xml_sat AS Xs ON Xs.UUID = X.UUID "
		SQL &= "WHERE   X.ID_Empresa= " & Me.lstCliente.SelectItem & " and X.UUID like '%" & Me.TxtFiltro.Text.Trim() & "%'"
		ds = Eventos.Obtener_DS(SQL)
		If ds.Tables(0).Rows.Count > 0 Then
			Me.Tabla.DataSource = ds.Tables(0)
			Tabla.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
			Tabla.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
			Tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
		Else
			RadMessageBox.Show("No hay Ordenes en el Periodo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
		End If
	End Sub
End Class