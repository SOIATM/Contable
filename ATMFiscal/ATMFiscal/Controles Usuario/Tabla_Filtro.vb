Imports System.Data.SqlClient
Imports Telerik.WinControls
Public Class Tabla_Filtro
    Implements System.Collections.IComparer
    Private sortOrderModifier As Integer = 1
    Private Col As Integer = 0
    Public Event Cerrar()
    Public Event Guardar()
    Public Event Nuevo()
    Public Event Agregar()
    Public Event Eliminar()
    Public Event Imprimir()
    Public Event Registro()
    Public Event Refrescar()
    Dim consul, sql, sql2, orden, filtro, almacenar, fechar, fecha, refres, otro, tablas, tablas2, distin, distin2 As String
    Dim difiltro As Boolean = True



    Dim accion, accionr, todo, p, q, hacer, inicio, made, unidad, historico, conFech As Boolean
    Public Property CmdActualizar_enabled() As Boolean
        Get
            Return Me.CmdActualizar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdActualizar.Enabled = value
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
    Public Property CmdNuevo_enabled() As Boolean
        Get
            Return Me.CmdNuevo.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdNuevo.Enabled = value
        End Set
    End Property
    Public Property Cmdguardar_enabled() As Boolean
        Get
            Return Me.CmdGuardar.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdGuardar.Enabled = value
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

    Public Property CmdExportaExcel_enabled() As Boolean
        Get
            Return Me.CmdExportaExcel.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.CmdExportaExcel.Enabled = value
        End Set
    End Property

    Public Sub Tablaconsulta(ByVal tabla As String, Optional ByVal tabla2 As String = "")
        tablas = tabla
        tablas2 = tabla2
    End Sub
    ''' <summary>
    ''' permite ordenar las consultas el ds 
    ''' </summary>
    ''' <param name="filtrar ">si el campo filtrar es verdadero se activa el filtro de ordenamiento</param>
    ''' <remarks></remarks>
    Public Sub Ordenar(Optional ByVal filtrar As Boolean = False, Optional ByVal campo As String = "")
        orden = campo
    End Sub
    Public Function Registro_columa(ByVal columna As Integer) As String
        Try
            Return Me.Tabla.Item(columna, Tabla.CurrentCell.RowIndex).Value.ToString
        Catch ex As Exception

        End Try

    End Function
    Private Sub Tabla_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Tabla.CellClick

        Dim columna As Integer = Me.Tabla.CurrentCell.ColumnIndex
        Me.lblCAMPO.Text = Me.Tabla.Columns(columna).HeaderText.ToString
        If Me.ComboAño.Text = "*" Then
            If inicio = True Then
                consul = sql ' almacena la consulta anterior y la iguala al sql
            Else
                consul = refres
            End If
            Me.lstfiltro.Cargar("select DISTINCT " & Me.lblCAMPO.Text & " ," & Me.lblCAMPO.Text & " from (" & consul & ") AS tabla2  order by tabla2." & Me.lblCAMPO.Text)
        Else
            If inicio = True Then
                consul = sql ' almacena la consulta anterior y la iguala al sql
            Else
                consul = consul
            End If
            Me.lstfiltro.Cargar("select DISTINCT " & Me.lblCAMPO.Text & " ," & Me.lblCAMPO.Text & " from (" & consul & ") AS tabla2  order by tabla2." & Me.lblCAMPO.Text)
        End If
        Me.lstfiltro.Text = ""
        Me.lstfiltro.SelectItem = 0
        made = False
        Me.Label5.Text = Tabla.RowCount
        If unidad = True Then ' si la variable unidad que es la que controla el evento de refrescar cuando es toda la consulta
            If distin <> "" Then
                filtro = Me.Tabla.Columns(columna).HeaderText.ToString
                consul = refres & " where " & filtro & " like '%" & Me.lstfiltro.SelectText & "%'"
                sql = consul
                inicio = True
            Else
                ' consul = refres & fechar & " like '%" & Me.txtfiltro.Text & "%'"
            End If
        Else
        End If
        ' si se da click en la celda quiere decir que se va a consultar y por tanto la variable accion se iguala a falso
        accion = False
        unidad = False
    End Sub
    Public Sub Filtrar()

        If hacer = True Then   ' si la etiqueta de titulos esta vacia no realizo el filtro
            If Me.lblCAMPO.Text = "" Then
            Else

                ' si el contenido de la etiqueta es diferente a "" entonces
                If difiltro = False And orden <> "" And accionr = True Then
                    'si difiltro es falso y accionr es verdadero quiere decir que es una consulta que seguira almacenando el mismo campo al final 
                    If filtro Like "Fecha*" Or filtro Like "fecha*" Then ' este if verifica si el campo seleccionado se trata de una fecha 
                        ' si se trata de una fecha entonces:
                        If Me.txtfiltro.TextLength = 10 And p = True Then ' se mide el contenido de la fecha para tomar unicamente los 10 primeros digitos es decir dia mes y año
                            sql = consul & " and  " & filtro & " = convert(datetime,'" & Me.lstfiltro.SelectText & "',103)" ' a la ultima consulta se le suma el nuevo campo fecha a flitrar
                            sql2 = sql & " order by " & orden ' se realiza el ordenamiento
                            Dim ds As DataSet = Eventos.Obtener_DS(sql2) ' se manda a un ds la consulta resultante 
                            Eventos.LlenarDataGrid_DS(ds, Tabla) ' se muestra en la tabla la consulta resultante
                            accion = False ' se pas ael valor en falso a la variable accion para que la siguiente vez se indique que ya est ael form abierto

                        ElseIf Me.txtfiltro.TextLength = 10 And p = False Then
                            'consul = sql ' almacen a la consulta anterior y la iguala al sql
                            consul = consul
                            sql = consul & " and  " & filtro & " = convert(datetime,'" & Me.lstfiltro.SelectText & "',103)" ' a la ultima consulta se le suma el nuevo campo fecha a flitrar
                            sql2 = sql & " order by " & orden ' se realiza el ordenamiento
                            Dim ds As DataSet = Eventos.Obtener_DS(sql2) ' se manda a un ds la consulta resultante 
                            Eventos.LlenarDataGrid_DS(ds, Tabla) ' se muestra en la tabla la consulta resultante
                            accion = False ' se pasa el valor en falso a la variable accion para que la siguiente vez se indique que ya est ael form abierto
                        End If
                    Else
                        If conFech = True Then
                            Dim filtrodd As String = Me.lstfiltro.SelectText
                            If Me.lstfiltro.SelectText = "True" Then
                                filtrodd = 1
                            ElseIf Me.lstfiltro.SelectText = "False" Then
                                filtrodd = 0
                            End If
                            sql = consul & " and  " & filtro & " = '" & filtrodd & "'" ' si la etiqueta del titulo anterior es igual a la de ahora se cambia el filtro anterior por el de ahora
                            sql2 = sql & " order by " & orden ' se realiza el ordenamiento
                            Dim ds As DataSet = Eventos.Obtener_DS(sql2) ' se manda a un ds la consulta resultante 
                            Eventos.LlenarDataGrid_DS(ds, Tabla) ' se muestra en la tabla la consulta resultante
                            accion = False ' se pas ael valor en falso a la variable accion para que la siguiente vez se indique que ya est ael form abierto
                            inicio = True
                        Else
                            Dim filtrodd As String = Me.lstfiltro.SelectText
                            If Me.lstfiltro.SelectText = "True" Then
                                filtrodd = 1
                            ElseIf Me.lstfiltro.SelectText = "False" Then
                                filtrodd = 0
                            End If
                            sql = consul & " and  " & filtro & " = '" & filtrodd & "'" ' si la etiqueta del titulo anterior es igual a la de ahora se cambia el filtro anterior por el de ahora
                            sql2 = sql & " order by " & orden ' se realiza el ordenamiento
                            Dim ds As DataSet = Eventos.Obtener_DS(sql2) ' se manda a un ds la consulta resultante 
                            LlenarDataGrid_DS(ds, Tabla) ' se muestra en la tabla la consulta resultante
                            accion = False ' se pas ael valor en falso a la variable accion para que la siguiente vez se indique que ya est ael form abierto
                            inicio = True
                        End If

                    End If
                Else
                    'si difiltro es verdadero y accionr es falso quiere decir que es una consulta que almacenara al final un nuevo filtro 
                    If inicio = True Then
                        consul = sql ' almacena la consulta anterior y la iguala al sql
                    Else
                        consul = consul
                    End If
                    Dim filtrodd As String = Me.lstfiltro.SelectText
                    If Me.lstfiltro.SelectText = "True" Then
                        filtrodd = 1
                    ElseIf Me.lstfiltro.SelectText = "False" Then
                        filtrodd = 0
                    End If
                    If conFech = True Then
                        sql = consul & " and  " & filtro & " = '" & filtrodd & "'" ' si la etiqueta del titulo anterior es igual a la de ahora se cambia el filtro anterior por el de ahora
                    Else
                        sql = consul & " where  " & filtro & " = '" & filtrodd & "'" ' si la etiqueta del titulo anterior es igual a la de ahora se cambia el filtro anterior por el de ahora
                    End If

                    sql2 = sql & " order by " & orden ' se realiza el ordenamiento
                    Dim ds As DataSet = Eventos.Obtener_DS(sql2) ' se manda a un ds la consulta resultante 
                    Eventos.LlenarDataGrid_DS(ds, Tabla) ' se muestra en la tabla la consulta resultante
                    accion = False ' se pasa el valor en falso a la variable accion para que la siguiente vez se indique que ya est ael form abierto
                    inicio = True
                End If
            End If
        End If
        Me.Label5.Text = Tabla.RowCount
    End Sub
    Private Sub TablaFiltro_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Ordenar()
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2016 Then
                Me.ComboAño.Items.Add(Str(i))
            End If
        Next
        Me.ComboAño.Items.Add("*")
        Me.ComboAño.Text = Str(DateTime.Now.Year)
        Dim mes = Now.Date.Month.ToString
        Me.ComboMes.Items.Add("*")
        If Len(mes) < 2 Then
            mes = "0" & mes
        End If
        Me.ComboMes.Text = mes
    End Sub
    Private Sub comboAño_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboAño.TextChanged
        otro = "where"
        If ComboAño.Text = "*" Then
            Me.ComboMes.Text = "*"
            todo = True
            fechar = fecha
        Else
            todo = False
            fechar = fecha
        End If
    End Sub
    Private Sub ComboMes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboMes.TextChanged
        If ComboMes.Text = "*" Then
            'Me.comboAño.Text = "*"
            'todo = True
            fechar = fecha
        Else
            todo = False
            fechar = fecha
        End If
    End Sub
    Public Sub cargarindependiente(ByVal consultas As String, Optional ByVal campo_fechas As String = "")
        'Dim consultas2 As String

        If inicio = True Then
        Else
            If orden = "" Then
                Dim ds As DataSet = Eventos.Obtener_DS(consultas & campo_fechas)
                'consul = consultas & campo_fechas
                Eventos.Ds_a_datagrid(ds, Tabla)
            Else
                Dim ds As DataSet = Eventos.Obtener_DS(consultas & campo_fechas & " order by " & orden)
                ' consul = consultas & campo_fechas
                Eventos.Ds_a_datagrid(ds, Tabla)
            End If
            hacer = False
        End If
        Me.Label5.Text = Tabla.RowCount
    End Sub


    Public Sub refresca()
        unidad = False
        If Me.ComboAño.Text = "*" Or historico = True Then
            todo = True
        Else
            todo = False
        End If
        Me.lstfiltro.Limpiar()

        hacer = False
        p = True
        Me.txtfiltro.Text = ""
        Me.lstfiltro.SelectItem = 0
        Me.lblCAMPO.Text = ""
        If distin <> "" Then
            fechar = distin
            fecha = distin
        Else
            fechar = fecha
        End If
        fechar = fecha

        If todo = True Then
            If orden = "" Then
                Dim ds As DataSet = Eventos.Obtener_DS(refres)
                LlenarDataGrid_DS(ds, Tabla)
                consul = refres & fechar & " like '%" & Me.txtfiltro.Text & "%'"
                sql = ""
                sql2 = ""
                difiltro = False
                accionr = True
                accion = True
                fechar = ""
            Else
                'Si la variable todo es verdadera y ademas la consulta a refrescar debe ser ordenada entonses se realizara lo siguiente
                Dim ds As DataSet = Eventos.Obtener_DS(refres & " order by " & orden)
                If ds.Tables(0).Rows.Count = 0 Then
                    Try
                        Tabla.Rows.Clear()
                    Catch ex As Exception

                    End Try

                Else
                    LlenarDataGrid_DS(ds, Tabla)
                End If

                sql = ""
                sql2 = ""
                difiltro = False
                accionr = True
                accion = True
                fechar = ""
                unidad = True
                inicio = True
            End If
        Else
            If distin <> "" Then
                fechar = distin
                fecha = distin
            Else
                fechar = fechar & ">=Convert(datetime,'01/" & Me.ComboMes.Text & "/" & Me.ComboAño.Text & "',103) "
            End If

            If orden = "" Then
                Dim ds As DataSet = Eventos.Obtener_DS(refres & fechar)
                LlenarDataGrid_DS(ds, Tabla)
                consul = refres & fechar
                sql = ""
                sql2 = ""
                difiltro = False
                accionr = True
                accion = True
                fechar = ""
            Else
                Dim ds As DataSet = Eventos.Obtener_DS(refres & fechar & " order by " & orden)
                If ds.Tables(0).Rows.Count = 0 Then
                    '  tabla.Rows.Clear()
                Else
                    LlenarDataGrid_DS(ds, Tabla)
                End If
                consul = refres & fechar
                sql = ""
                sql2 = ""
                difiltro = False
                accionr = True
                accion = True
                fechar = ""
            End If
        End If
        Me.Label5.Text = Tabla.RowCount
        inicio = False
    End Sub



    Private Sub Permisos(ByVal Modulos As String)
        Dim modulo() As String = Modulos.Split(";")
        Dim ds_permisos As DataSet
        Dim user As String = My.Forms.Inicio.LblUsuario.Text
        If user = "No Conectado" Then
            ds_permisos = Obtener_DS("Select " & modulo(0) & " from Usuarios where Usuario='humberto'")
        Else
            ds_permisos = Obtener_DS("Select " & modulo(0) & " from Usuarios where Usuario='" & My.Forms.Inicio.LblUsuario.Text & "'")
        End If

        If ds_permisos.Tables(0).Rows(0)(0) = "M" Then
            Me.CmdEliminar.Enabled = True
            Me.CmdGuardar.Enabled = True
            Me.CmdNuevo.Enabled = True
        Else
            Me.CmdEliminar.Enabled = False
            Me.CmdGuardar.Enabled = False
            Me.CmdNuevo.Enabled = False
        End If
        If modulo.GetLength(0) > 1 Then
            For i As Integer = 1 To modulo.GetLength(0) - 1
                ds_permisos = Obtener_DS("Select " & modulo(i) & " from Usuarios where Usuario='" & My.Forms.Inicio.LblUsuario.Text & "'")
                If ds_permisos.Tables(0).Rows(0)(0) = "M" Then
                    Me.CmdEliminar.Enabled = Me.CmdEliminar.Enabled Or True
                    Me.CmdGuardar.Enabled = Me.CmdGuardar.Enabled Or True
                    Me.CmdNuevo.Enabled = Me.CmdNuevo.Enabled Or True
                Else
                    Me.CmdEliminar.Enabled = Me.CmdEliminar.Enabled Or False
                    Me.CmdGuardar.Enabled = Me.CmdGuardar.Enabled Or False
                    Me.CmdNuevo.Enabled = Me.CmdNuevo.Enabled Or False
                End If
            Next
        End If
    End Sub
    Private Sub cmdEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdEliminar.Click
        If Eventos.Permiso(Me.Tag.ToString) Then
            If Tabla.RowCount > 0 Then
                RaiseEvent Eliminar()
                refresca()
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

        End If
    End Sub

    Private Sub cmdExportaExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExportaExcel.Click
        If Tabla.RowCount > 0 Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.info)
            Me.Text = Ms.ToString()

            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Text = "Exportando a Excel"
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
                frm.Barra.value = i
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
    Public ReadOnly Property fecha_año() As String
        Get
            Return Me.ComboAño.Text
        End Get
    End Property
    Public ReadOnly Property fecha_mes() As String
        Get
            Return Me.ComboMes.Text
        End Get
    End Property
    Public Property SqlSelect() As String
        Get
            Return Me.lblSqlSelect.Text
        End Get
        Set(ByVal value As String)
            Me.lblSqlSelect.Text = value
        End Set
    End Property
    Public ReadOnly Property SQLFiltrada() As String
        Get
            Dim sql As String
            sql = Me.lblSqlSelect.Text
            Dim dias_mes As Integer, mes As Integer = Val(Me.ComboMes.Text)
            If mes = 1 Or mes = 3 Or mes = 5 Or mes = 7 Or mes = 8 Or mes = 10 Or mes = 12 Then
                dias_mes = 31
            End If
            If mes = 4 Or mes = 6 Or mes = 9 Or mes = 11 Then
                dias_mes = 30
            End If
            If mes = 2 Then
                dias_mes = 29
            End If

            If Me.ComboMes.Text <> "*" And Me.ComboAño.Text <> "*" Then
                sql = sql & " Where " & Me.lblSqlInsert.Text & " >= convert(varchar,'01/" & Me.ComboMes.Text & "/" & Me.ComboAño.Text & "',103) and " & Me.lblSqlInsert.Text & " <= convert(varchar,'" & dias_mes & "/" & Me.ComboMes.Text & "/" & Me.ComboAño.Text & "',103)"
            End If
            If Me.ComboMes.Text = "*" And Me.ComboAño.Text <> "*" Then
                sql = sql & " Where " & Me.lblSqlInsert.Text & " >= convert(varchar,'01/01/" & Me.ComboAño.Text & "',103) and " & Me.lblSqlInsert.Text & " <= convert(varchar,'31/12/" & Me.ComboAño.Text & "',103)"
            End If
            Return sql
        End Get
    End Property
    Private Sub cmdguardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        If Eventos.Permiso(Me.Tag.ToString) Then
            RaiseEvent Guardar()
            refresca()
        Else
            Eventos.Hablar_sistema("No tienes permiso para modificar la información")
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

        End If
    End Sub
    Private Sub cmdActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdActualizar.Click
        Dim ds_permisos As DataSet = Obtener_DS("Select " & Me.Tag.ToString & " from Usuarios where Usuario='" & My.Forms.Inicio.LblUsuario.Text & "'")
        If ds_permisos.Tables(0).Rows(0)(0) = "M" Or ds_permisos.Tables(0).Rows(0)(0) = "C" Then
            RaiseEvent Refrescar()
            refresca()
        Else
            Eventos.Hablar_sistema("No tienes permiso para modificar la información")
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()
        End If

    End Sub
    Private Sub cmdNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdNuevo.Click
        If Eventos.Permiso(Me.Tag.ToString) Then
            RaiseEvent Nuevo()
            refresca()
        Else
            Eventos.Hablar_sistema("No tienes permiso para modificar la información")
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()
        End If

    End Sub

    Public Sub suc(ByVal colum As Integer, Optional ByVal campo As String = "")

        Dim existe As Boolean = False
        For i As Integer = 0 To Me.Tabla.Columns.Count - 1
            If Me.Tabla.Columns(i).HeaderText = campo Then
                existe = True
            End If
        Next
        If existe = True Then
            ' Me.tabla.Sort(Me.tabla.Columns(colum), System.ComponentModel.ListSortDirection.Ascending)
            Me.Tabla.Columns(colum).SortMode = DataGridViewColumnSortMode.Programmatic

        End If

    End Sub
    Private Sub tabla_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Tabla.ColumnHeaderMouseClick
        Ordenar(Tabla.Columns(e.ColumnIndex).HeaderText.ToString)
        Dim columna As Integer = e.ColumnIndex
        Dim acelerador As String = Tabla.Columns(e.ColumnIndex).HeaderText
        ' suc(columna, acelerador)
        ' Check which column is selected, otherwise set NewColumn to Nothing. 
        Dim newColumn As DataGridViewColumn
        If Tabla.Columns.GetColumnCount(DataGridViewElementStates.Selected) = 1 Then
            newColumn = Tabla.SelectedColumns(0)
        Else
            newColumn = Nothing
        End If

        Dim oldColumn As DataGridViewColumn = Tabla.SortedColumn
        Dim direction As System.ComponentModel.ListSortDirection

        ' If oldColumn is null, then the DataGridView is not currently sorted. 
        If oldColumn IsNot Nothing Then

            ' Sort the same column again, reversing the SortOrder. 
            If oldColumn Is newColumn AndAlso Tabla.SortOrder = SortOrder.Ascending Then
                direction = System.ComponentModel.ListSortDirection.Descending
            Else
                ' Sort a new column and remove the old SortGlyph.
                direction = System.ComponentModel.ListSortDirection.Ascending
                '  oldColumn.HeaderCell.SortGlyphDirection = SortOrder.Unspecified
            End If
        Else
            direction = System.ComponentModel.ListSortDirection.Ascending
        End If


    End Sub
    Public Sub NUEVO2(ByVal sortOrder As SortOrder, ByVal columna As Integer)
        Col = columna
        If sortOrder = SortOrder.Descending Then
            sortOrderModifier = -1
        ElseIf sortOrder = SortOrder.Ascending Then
            sortOrderModifier = 1
        End If
    End Sub
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
        Implements System.Collections.IComparer.Compare
        Dim DataGridViewRow1 As DataGridViewRow = CType(x, DataGridViewRow)
        Dim DataGridViewRow2 As DataGridViewRow = CType(y, DataGridViewRow)
        Return (Math.Sign(CLng(DataGridViewRow1.Cells(Col).Value) - CLng(DataGridViewRow2.Cells(Col).Value))) * sortOrderModifier
    End Function
    Public Sub Cargar2(ByVal modulo As String, ByVal consulta As String, Optional ByVal campo_fecha As String = "", Optional ByVal campofiltros As String = "", Optional ByVal campo_distinto As String = "", Optional ByVal conFecha As Integer = 1)
        conFech = Eventos.Bool2(conFecha)
        Me.lblSqlInsert.Text = campofiltros
        ' si el campo distinto esta vacio quiere decir que se generara una consulta de HISTORICO
        distin = campo_distinto
        ' LA VARIABLE INICIO NOS INDICA CUANDO SE EJEJUTA EL CODIGO DE CARGAR
        inicio = True
        ' LA VARIABLE HACER CONTROLA EL FILTRO DE LAS CONSULTAS
        hacer = False
        ' LA VARIABLE REFRES ALMACENA LA CONSULTA ORIGINAL PARA LLAMARLA AL MOMENTO DE ACTUALIZAR INFORMACION
        refres = consulta

        accion = True
        ' LA VARIABLE TODO INDICA QUE SE DEBERA REALIAR LA CONSULTA DE EL HISTORICO
        historico = True
        p = True
        If campo_fecha <> "" Then
            fechar = campo_fecha
            fecha = campo_fecha
            campo_fecha = campo_fecha & ">=Convert(datetime,'01/" & DateTime.Now.Month & "/" & DateTime.Now.Year & "',103)"
        Else
            campo_fecha = ""
            fechar = campo_fecha
            fecha = campo_fecha
        End If

        If campo_distinto <> "" Then
            fechar = campo_distinto
            fecha = campo_distinto
            campo_fecha = campo_distinto

        Else
            campo_fecha = ""
            fechar = campo_distinto
            fecha = campo_distinto
        End If


        If orden = "" Then
            Dim ds As DataSet = Eventos.Obtener_DS(consulta)
            consul = consulta & campo_fecha
            LlenarDataGrid_DS(ds, Tabla)
        Else
            Dim ds As DataSet = Eventos.Obtener_DS(consulta & " order by " & orden)
            consul = consulta & campo_fecha
            Dim a As Integer = ds.Tables(0).Rows.Count
            LlenarDataGrid_DS(ds, Tabla)
        End If

        inicio = False
        Permisos(modulo)
        Me.Label5.Text = Tabla.RowCount
        ' se controla el where de la consulta historico para las listas de un form 
        unidad = True
    End Sub
    Private Sub lstfiltro_Enters() Handles lstfiltro.Enters, lstfiltro.Enters
        hacer = True
        ' le asigno el valor de la label al una variable 
        Dim asu As String = Me.lblCAMPO.Text
        'comparo el valor de la etiqueta
        If Me.lblCAMPO.Text = "" Then
            ' si la etiqueta esta vacia no realizo ninguna operacion
        Else
            'si la etiqueta contien un titulo se realiza lo siguiente
            If accion = False Then
                ' si la variable de accion es falsa se compran las dos variables para saber si la consulta almacenara un campo nuevo o si es la consulta principal
                If asu <> filtro Then
                    '  Consulta secundaria
                    difiltro = True
                    accionr = False

                    p = False

                Else
                    'consulta principal
                    difiltro = False
                    accionr = True
                End If

            Else
                ' si la variable de accion es verdadera quiere decir qeu se trata de cargar la consulta principal 
                difiltro = False
                accionr = True
            End If
        End If
        ' se le asigna a la variable filtro el nuevo campo a consultar
        filtro = Me.lblCAMPO.Text
        Me.txtfiltro.Text = Me.lstfiltro.SelectText

        'made = True
        ' se le hace llamada al evento filtrar y se pasa  los valores de : *****difiltro y accionr****** asi como *****filtro******
        Filtrar()
    End Sub
    Public Sub Cargar(ByVal modulo As String, ByVal consulta As String, Optional ByVal campo_fecha As String = "", Optional ByVal campofiltros As String = "", Optional ByVal campo_distinto As String = "", Optional ByVal conFecha As Integer = 1)
        historico = False
        Me.lblSqlInsert.Text = campofiltros
        distin = campo_distinto
        inicio = True
        hacer = False
        refres = consulta
        accion = True
        todo = False
        p = True
        conFech = Eventos.Bool2(conFecha)
        If campo_fecha <> "" Then
            fechar = campo_fecha
            fecha = campo_fecha
            campo_fecha = campo_fecha & ">=Convert(datetime,'01/" & DateTime.Now.Month & "/" & DateTime.Now.Year & "',103)"
        Else
            campo_fecha = ""
            fechar = campo_fecha
            fecha = campo_fecha
        End If
        If campo_distinto <> "" Then
            fechar = campo_distinto
            fecha = campo_distinto
            campo_fecha = campo_distinto

        Else
            campo_fecha = ""
            fechar = campo_distinto
            fecha = campo_distinto
        End If
        ' si la consult aalmacena los campos de año y mes con * quiere decir que se debera mostrar todo
        If Me.ComboAño.Text = "*" Then
            todo = True
        Else
            todo = False
        End If
        If todo = True Then
            If orden = "" Then
                Dim ds As DataSet = Eventos.Obtener_DS(consulta)
                consul = consulta & campo_fecha
                Eventos.LlenarDataGrid_DS(ds, Tabla)
            Else
                Dim ds As DataSet = Eventos.Obtener_DS(consulta & " order by " & orden)
                consul = consulta & campo_fecha
                Eventos.LlenarDataGrid_DS(ds, Tabla)
            End If
        Else
            If orden = "" Then
                Dim ds As DataSet = Eventos.Obtener_DS(consulta & campo_fecha)
                consul = consulta & campo_fecha
                Eventos.LlenarDataGrid_DS(ds, Tabla)
            Else
                Dim ds As DataSet = Eventos.Obtener_DS(consulta & campo_fecha & " order by " & orden)
                consul = consulta & campo_fecha
                Eventos.LlenarDataGrid_DS(ds, Tabla)
            End If
        End If

        inicio = False
        Permisos(modulo)
        Me.Label5.Text = Tabla.RowCount
        unidad = False
    End Sub
    Public Sub Ordenar(ByVal campo As String)
        If campo = "ID_orden" Then

            Dim existe As Boolean = False
            For i As Integer = 0 To Me.Tabla.Columns.Count - 1
                If Me.Tabla.Columns(i).Name = campo Then
                    existe = True
                End If
            Next
            If existe = True Then
                Me.Tabla.Sort(Me.Tabla.Columns("consecutivo_ot"), System.ComponentModel.ListSortDirection.Ascending)
            End If
        End If
    End Sub
    Private Sub cmdCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCerrar.Click
        RaiseEvent Cerrar()
    End Sub



    Private Sub tabla_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Tabla.CellDoubleClick
        made = True
        Dim ds_permisos As DataSet = Obtener_DS("Select " & Me.Tag.ToString & " from Usuarios where Usuario='" & My.Forms.Inicio.LblUsuario.Text & "'")
        If ds_permisos.Tables(0).Rows(0)(0) = "M" Or ds_permisos.Tables(0).Rows(0)(0) = "C" Then
            Me.CmdActualizar.Enabled = True
            RaiseEvent Registro()
        Else
            Eventos.Hablar_sistema("No tienes permiso para modificar la información")
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()
        End If
    End Sub

    Private Sub CmdCerrar_ToolTipTextNeeded(sender As Object, e As ToolTipTextNeededEventArgs) Handles CmdCerrar.ToolTipTextNeeded
        e.ToolTip.InitialDelay = 1000
        e.ToolTip.IsBalloon = True
        e.ToolTipText = "Cierra la Ventana Actual"
        e.ToolTip.ToolTipIcon = ToolTipIcon.Info
    End Sub
End Class
