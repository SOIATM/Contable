Imports System.Data.SqlClient
Imports Telerik.WinControls
Public Class tabla_detalle
    Public Event Cmd_Nuevo()
    Public Event Cerrar()
    Public Event Cmd_Editar(ByVal clave As String)
    Public Event Cmd_eliminar(ByVal clave As String)
    Public Event Registro(ByVal clave As String)

    Public Property Registro_columna(ByVal columna As Integer) As String
        Get
            Return Valor(Me.Tabla.Item(columna, Me.Tabla.CurrentCell.RowIndex).Value)
        End Get
        Set(ByVal value As String)
            Me.Tabla.Item(columna, Me.Tabla.CurrentCell.RowIndex).Value = value
        End Set
    End Property
    Public Property SqlSelect() As String
        Get
            Return Me.lblselect.Text
        End Get
        Set(ByVal value As String)
            Me.lblselect.Text = value
        End Set
    End Property
    Public Property CmdNuevo_Enabled() As Boolean
        Get
            Return Me.CmdAgregar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdAgregar.Enabled = value
        End Set
    End Property
    Public Property CmdEditar_Enabled() As Boolean
        Get
            Return Me.CmdEditar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdEditar.Enabled = value
        End Set
    End Property
    Public Property CmdEliminar_enabled() As Boolean
        Get
            Return Me.CmdEliminar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdEliminar.Enabled = value
        End Set
    End Property
    Public Property CmdRefrescar_enabled() As Boolean
        Get
            Return Me.CmdRefrescar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdRefrescar.Enabled = value
        End Set
    End Property
    Public Property Cmdcerrar_enabled() As Boolean
        Get
            Return Me.CmdCerrar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdCerrar.Enabled = value
        End Set
    End Property
    Public Sub Cargar()
        Refrescar()
    End Sub
    Public Sub Refrescar()
        Dim con As New SqlConnection("Data Source=" & Trim(My.Forms.Inicio.txtServerDB.Text) & "" & ";Initial Catalog=Contable;Persist Security Info=True;User=ContaP;Password=CpDb2018")
        Try
            con.Open()
            Dim ds As New SqlDataAdapter(Me.lblselect.Text, con)
            Dim ds2 As New DataSet
            ds.Fill(ds2, "TablaDetalle")
            Me.Tabla.DataSource = ds2
            Me.Tabla.DataMember = "TablaDetalle"
            con.Close()
        Catch ex As Exception
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Error al cargar los datos de la tabla de detalle:: ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

        End Try
        Me.txtregistros.Text = Me.Tabla.RowCount
    End Sub

    Private Sub CmdRefrescar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRefrescar.Click
        Refrescar()
    End Sub

    Private Sub CmdAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        If Permiso(Me.Tag.ToString) Then
            RaiseEvent Cmd_Nuevo()
            Refrescar()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

        End If
    End Sub

    Private Sub CmdEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdEditar.Click
        If Permiso(Me.Tag.ToString) Then
            If Tabla.RowCount > 0 Then
                RaiseEvent Cmd_Editar(Tabla.Item(0, Tabla.CurrentCell.RowIndex).Value)
                Refrescar()
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()

            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()
        End If
    End Sub

    Private Sub CmdEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdEliminar.Click
        If Permiso(Me.Tag.ToString) Then
            If Tabla.RowCount > 0 Then
                RaiseEvent Cmd_eliminar(Tabla.Item(0, Tabla.CurrentCell.RowIndex).Value)
                Refrescar()
            Else

                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "No hay registros para eliminar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()
        End If
    End Sub


    Private Sub CmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExcel.Click
        Refrescar()
        If Tabla.RowCount > 0 Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.info)
            Me.Text = Ms.ToString()
            Dim frm As New DialogExpExcel
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tabla.RowCount - 1
            Dim m_Excel As Microsoft.Office.Interop.Excel.Application
            '' Creamos un objeto WorkBook 
            Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook
            '' Creamos un objeto WorkSheet
            Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet
            '' Iniciamos una instancia a Excel
            m_Excel = New Microsoft.Office.Interop.Excel.Application
            m_Excel.Visible = False
            '' Creamos una instancia del Workbooks de Excel
            '' Creamos una instancia de la primera hoja de trabajo de Excel
            objLibroExcel = m_Excel.Workbooks.Add()
            objHojaExcel = objLibroExcel.Worksheets(1)
            objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            '' Hacemos esta hoja la visible en pantalla 
            '' (como seleccionamos la primera esto no es necesario
            '' si seleccionamos una diferente a la primera si lo
            '' necesitaríamos, esto lo hacemos como forma de mostrar como cambiar de entre hojas en un documento Excel).
            objHojaExcel.Activate()
            Dim i As Integer, j As Integer
            For i = 0 To Tabla.Columns.Count - 1
                objHojaExcel.Cells(1, i + 1) = Me.Tabla.Columns.Item(i).HeaderCell.Value
            Next
            For i = 0 To Tabla.RowCount - 1
                'frm.Barra.Value = i
                For j = 0 To Tabla.Columns.Count - 1
                    objHojaExcel.Cells(i + 2, j + 1) = Me.Tabla.Item(j, i).Value
                Next
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Ms = RadMessageBox.Show(Me, "Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Text = Ms.ToString()
            m_Excel.Visible = True
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.info)
            Me.Text = Ms.ToString()
        End If
    End Sub



    Private Sub Tabla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tabla.Click
        If Tabla.RowCount > 0 Then
            RaiseEvent Registro(Tabla.Item(0, Tabla.CurrentCell.RowIndex).Value.ToString)
        End If
    End Sub

    Private Sub CmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
        RaiseEvent Cerrar()
    End Sub


End Class

