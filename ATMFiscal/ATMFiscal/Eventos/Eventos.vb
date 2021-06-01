Imports System
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Speech
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Reflection
Imports Telerik.WinControls
Module Eventos
    Public versionDB As String = "C190913"
    Public titulo_app As String = "Fiscal HLJ Systems " & versionDB
    'SendKeys.Send("{Enter}")
    ''' <summary>
    ''' Esta clase permite controlar los colores usados en el menu principal
    ''' </summary>
    Public Class ColoresMenus

        Inherits ProfessionalColorTable

        Public Overrides ReadOnly Property MenuBorder() As System.Drawing.Color
            Get
                Return Color.DarkGray
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelected() As System.Drawing.Color
            Get
                Return Color.LightCoral
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemPressedGradientEnd() As Color
            Get
                Return Color.LightGray
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientBegin() As Color
            Get
                Return Color.LightGray
            End Get
        End Property

        Public Overrides ReadOnly Property ToolStripDropDownBackground() As Color
            Get
                Return Color.LightGray
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemBorder() As Color
            Get
                Return Color.DarkGray
            End Get
        End Property
    End Class

    ''' <summary>
    ''' Esta clase permite Abrir dentro del formulario MDI un nuevo form
    ''' </summary>
    ''' <param name="formulario"></param>
    Public Sub Abrir_form(ByVal formulario As Form)
        If Inicio.ActiveMdiChild IsNot Nothing Then
            Inicio.ActiveMdiChild.WindowState = FormWindowState.Normal
        End If
        formulario.MdiParent = Inicio
        formulario.StartPosition = FormStartPosition.CenterScreen
        formulario.WindowState = FormWindowState.Normal
        formulario.Show()
    End Sub
    ''' <summary>
    ''' Esta clase permite llenar un DataGridView con la informacion contenida en un DataSet
    ''' </summary>
    ''' <param name="ds">  </param>
    ''' <param name="Tabla"> </param>
    Public Sub LlenarDataGrid_DS(ByVal ds As DataSet, ByVal Tabla As Windows.Forms.DataGridView)
        Try
            Tabla.DataSource = ds
            Tabla.DataMember = ds.Tables(0).TableName
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Esta funcion inyecta un string a un conector SQL para solicitar informacion a la DB y lo almacena en un DataSet que es retornado
    ''' </summary>
    ''' <param name="sql"> esta es una variable de tipo string que se mandara a la BD </param>
    ''' <param name="server"></param>
    ''' <param name="user"></param>
    ''' <param name="pass"></param>
    ''' <returns></returns>
    Public Function Obtener_DS(ByVal sql As String, Optional ByVal server As String = "2.tcp.ngrok.io,13130", Optional ByVal user As String = "ContaP", Optional ByVal pass As String = "CpDb2018") As DataSet
        Dim source As String = "Data Source=tcp:" & Trim(My.Forms.Inicio.txtServerDB.Text) & ";Persist Security Info=True;User ID=" & user & ";password=" & pass & ";Initial Catalog=Contable"

        Dim Conn As New SqlConnection(source)
        Dim dA1 As New SqlDataAdapter(sql, Conn)
        Dim dS1 As New DataSet
        Try
            Conn.Open()
            dA1.SelectCommand.CommandTimeout = 0
            dA1.Fill(dS1, "Temp")
            Conn.Close()
            Return dS1
        Catch ex As Exception
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show("Error consulte a Sistemas...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Return dS1
        End Try

    End Function
    ''' <summary>
    ''' Esta funcion recibe un objeteto y lo evalua para devolver valores quitando los nulos y vacios.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function Valor(ByVal value As Object) As Object
        If IsDBNull(value) Then
            Return ""
        Else
            If IsNothing(value) Then
                Return ""
            Else
                Return value
            End If
        End If
    End Function
    ''' <summary>
    ''' Esta funcion recibe como parametro un valor Booleno y retorna un 0 en caso de false y 1 en caso de un true
    ''' </summary>
    ''' <param name="valor"></param>
    ''' <returns></returns>
    Public Function Bool2(ByVal valor As Boolean) As Integer
        If valor Then
            Return 1
        Else
            Return 0
        End If
    End Function
    ''' <summary>
    ''' Esta clase permite enviar un mensaje de tipo voz por parte del sistema
    ''' </summary>
    ''' <param name="texto"></param>
    Public Sub Hablar_sistema(ByVal texto As String)
        Dim audio = CreateObject("SAPI.spvoice")
        With audio
            .voice = .getvoices.item(2)
            .Volume = 100
            .Rate = -1
            .speak(texto)
        End With
    End Sub
    ''' <summary>
    ''' Esta clase permite evaluar los permisos que tiene un usuario para el usos del sistema recibe el tipo de permiso separado por ";" en caso de querer evaluar mas de uno
    ''' </summary>
    ''' <param name="Mods"></param>
    ''' <returns></returns>
    Public Function Permiso(ByVal Mods As String) As Boolean
        Dim valor As Boolean
        Dim modulo() As String = Mods.Split(";")
        Dim ds_permisos As DataSet = Obtener_DS("Select " & modulo(0) & " from Usuarios where Usuario='" & My.Forms.Inicio.LblUsuario.Text & "'")
        If ds_permisos.Tables(0).Rows(0)(0) = "M" Then
            valor = True
        Else
            valor = False
        End If
        If modulo.GetLength(0) > 1 Then
            For i As Integer = 1 To modulo.GetLength(0) - 1
                ds_permisos = Obtener_DS("Select " & modulo(i) & " from Usuarios where Usuario='" & My.Forms.Inicio.LblUsuario.Text & "'")
                If ds_permisos.Tables(0).Rows(0)(0) = "M" Then
                    valor = valor Or True
                Else
                    valor = valor Or False
                End If
            Next
        End If
        Return valor
    End Function
    ''' <summary>
    ''' Esta funcion llena un DataGridView con información contenida en un DataSet y devuelve la respuesta en tipo Booleano 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="Tabla"></param>
    ''' <returns></returns>
    Public Function Ds_a_datagrid(ByVal ds As DataSet, ByVal Tabla As DataGridView) As Boolean
        If ds.Tables(0).Rows.Count > 0 Then
            Tabla.RowCount = ds.Tables(0).Rows.Count
            Tabla.ColumnCount = ds.Tables(0).Columns.Count
            For j As Integer = 0 To ds.Tables(0).Columns.Count - 1
                Tabla.Columns(j).HeaderText = ds.Tables(0).Columns(j).Caption
            Next
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                For j As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    Tabla.Item(j, i).Value = ds.Tables(0).Rows(i)(j)
                Next
            Next
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>
    ''' Esta funcion permite obtener un valor o valores de la base de datos mediante parametros en especifico
    ''' </summary>
    ''' <param name="Tabla"></param>
    ''' <param name="Campo"></param>
    ''' <param name="Where"></param>
    ''' <param name="RegresaUnicoValor"></param>
    ''' <returns></returns>
    Public Function ObtenerValorDB(ByVal Tabla As String, ByVal Campo As String, ByVal Where As String, ByVal RegresaUnicoValor As Boolean) As Object
        Dim sql As String = "Select " & Campo & " from " & Tabla & " Where " & Where
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If RegresaUnicoValor Then
            If ds.Tables(0).Rows.Count = 0 Then
                Return " "
            Else
                Return ds.Tables(0).Rows(0)(0)
            End If

        Else
            Dim arreglo() As Object
            ReDim arreglo(ds.Tables(0).Rows.Count)
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                arreglo(i) = ds.Tables(0).Rows(i)(0)
            Next
            Return arreglo
        End If
    End Function
    ''' <summary>
    ''' Esta funcion realiza una SqlConnection e inyecta un string a la base de datos  y retorna la respuesta del servidor 
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    Public Function Comando_sql(ByVal sql) As Integer
        Dim source As String
        source = "Data Source= " & Trim(My.Forms.Inicio.txtServerDB.Text) & " ;Persist Security Info=True;User ID=ContaP;password=CpDb2018;Initial Catalog=Contable"
        Dim Conn As New SqlConnection(source)
        Try
            Conn.Open()
            Dim cmdsql As New SqlClient.SqlCommand
            cmdsql.CommandText = sql
            cmdsql.Connection = Conn
            Comando_sql = cmdsql.ExecuteNonQuery()
            Conn.Close()
        Catch ex As Exception
            Comando_sql = 0
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show("No se pudieron guardar los datos: " & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

        End Try
        Return Comando_sql
    End Function
    ''' <summary>
    ''' Esta funcion perite dar formato a un string de tipo fecha en el formato adecuado para base de datos, en caso de que el string sea "NULL" el sistema asigna el día actual.
    ''' </summary>
    ''' <param name="fecha"></param>
    ''' <returns></returns>
    Public Function Sql_hoy(Optional ByVal fecha As String = "NULL") As String
        If fecha = "NULL" Then
            Return "convert(datetime,'" & DateTime.Now.ToString.Substring(0, 10) & "',103)"
        Else
            If fecha = "  /  /" Or fecha = "" Then
                Return "NULL"
            Else
                If fecha.Length >= 10 Then
                    Return "convert(datetime,'" & fecha.Substring(0, 10) & "',103)"
                Else
                    Return "convert(datetime,'" & fecha & "',103)"
                End If
            End If
        End If
    End Function
    ''' <summary>
    ''' Esta funcion inserta en la base de datos el string con la información que el usuario utilizo en el sistema.
    ''' </summary>
    ''' <param name="tabla"></param>
    ''' <param name="Descripcion"></param>
    ''' <returns></returns>
    Public Function Insertar_usuariol(ByVal tabla As String, ByVal Descripcion As String) As Boolean
        Dim mov As String = Descripcion.Replace("'", "-")
        Dim sql As String = "INSERT INTO dbo.UserMov"
        sql &= "("
        sql &= " ID_Usuario,"
        sql &= " Fecha,"
        sql &= " hora,"
        sql &= " tabla,"
        sql &= " Descripcion"
        sql &= " )"
        sql &= " VALUES "
        sql &= "("
        sql &= "'" & Eventos.TruncarCadena(Eventos.Nombre_Usuario, 20) & "',"
        sql &= " " & Eventos.Sql_hoy() & ","
        sql &= "'" & DateTime.Now.Hour.ToString & ":" & DateTime.Now.Minute.ToString & " ' ,"
        sql &= "'" & tabla & "',"
        sql &= "'" & mov & "'"
        sql &= ")"

        If Comando_sql(sql) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Ds_valor(ByVal ds As DataSet, ByVal fila As Integer, ByVal columna As Integer, Optional ByVal tabla As Integer = 0) As String
        Ds_valor = Valor(ds.Tables(tabla).Rows(fila)(columna))
    End Function

    Public Function Empresas_de_usuario(ByVal usuario As String)
        Dim empresa As Integer

        Return empresa
    End Function
    Public Function TruncarCadena(ByVal Cadena As String, ByVal NumCaracteres As Integer) As String
        If Cadena.Length < NumCaracteres Then
            Return Cadena
        Else
            Return Cadena.Substring(0, NumCaracteres)
        End If
    End Function
    Public Function Nombre_Usuario() As String
        If Inicio.LblUsuario.Text = "No Conectado" Then
            Return "No Conectado"
        Else
            Return Eventos.Obtener_DS("SELECT usuario FROM Usuarios WHERE Usuario='" & Inicio.LblUsuario.Text & "'").Tables(0).Rows(0)(0)
        End If
    End Function
    ''' <summary>
    ''' Esta clase permite insertar un nuevo tipo de poliza para Auditoria SAT
    ''' </summary>
    ''' <param name="Descrip"></param>
    ''' <param name="Descripcion"></param>
    ''' <param name="claves"></param>
    ''' <param name="Cliente"></param>
    Public Sub InsertarTipo_poliza(ByVal Descrip As String, ByVal Descripcion As String, ByVal claves As String, ByVal Cliente As Integer)
        Dim sql As String = "  INSERT INTO dbo.Tipos_Poliza_Sat"
        sql &= "("
        sql &= " 	Clave ,"
        sql &= " 	Id_Cliente ,"
        sql &= " 	Id_Tipo_poliza ,"
        sql &= " 	Nombre"
        sql &= " 	)"
        sql &= " VALUES "
        sql &= "("
        sql &= " 	'" & claves & "'," '
        sql &= " 	" & Cliente & "," '
        sql &= " 	" & Descrip & "," '
        sql &= " 	'" & Descripcion & "'" '
        sql &= " 	)"
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Tipos_Poliza_Sat_I", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

        End If
    End Sub
    ''' <summary>
    ''' Esta clase muestra el archivo excel recien creado
    ''' </summary>
    ''' <param name="excel"></param>
    Public Sub Mostrar_Excel(ByVal excel As Microsoft.Office.Interop.Excel.Application)
        excel.Visible = True
    End Sub
    ''' <summary>
    ''' Esta clase permite escribor en un archivo excel activo
    ''' </summary>
    ''' <param name="ObjExcel"></param>
    ''' <param name="Fila"></param>
    ''' <param name="columna"></param>
    ''' <param name="Valor"></param>
    ''' <param name="Hoja"></param>
    Public Sub EscribeExcel(ByVal ObjExcel As Microsoft.Office.Interop.Excel.Application, ByVal Fila As Integer, ByVal columna As Integer, ByVal Valor As String, Optional Hoja As Integer = 1)
        Try
            ObjExcel.Workbooks(1).Worksheets(Hoja).Cells(Fila, columna) = Valor
            ObjExcel = Nothing
        Catch ex As Exception

            Dim Ms As DialogResult = RadMessageBox.Show(ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

        End Try
    End Sub
    ''' <summary>
    ''' Esta funcion permite crear un nuevo archivo de Excel a partir de una plantilla del sistema, ya sea vacía o con formato en especifico
    ''' </summary>
    ''' <param name="plantilla"></param>
    ''' <param name="visible"></param>
    ''' <param name="empresa"></param>
    ''' <returns></returns>
    Public Function NuevoExcel(ByVal plantilla As String, ByVal visible As Boolean, Optional ByVal empresa As Integer = 1) As Microsoft.Office.Interop.Excel.Application
        Dim m_Excel As Microsoft.Office.Interop.Excel.Application
        Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook
        Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet
        Dim archivo As String
        If empresa = 0 Or empresa = 1 Then
            archivo = Application.StartupPath & IIf(empresa = 1, "\Plantillas\Contable", "\Plantillas") & plantilla & ".xlsx"
        Else
            archivo = Application.StartupPath & IIf(My.Forms.Inicio.TxtEmpresa.Text = "ATMFiscal", "\Plantillas", "\Plantillas") & plantilla & ".xlsx"
        End If

        Try
            m_Excel = New Microsoft.Office.Interop.Excel.Application
            m_Excel.Visible = False
            objLibroExcel = m_Excel.Workbooks.Add(archivo)
            objHojaExcel = objLibroExcel.Worksheets(1)
            objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            objHojaExcel.Activate()
            m_Excel.Visible = visible
        Catch ex As Exception

            RadMessageBox.Show("Error al crear el archivo de Excel: ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

            m_Excel = Nothing
        End Try
        Return m_Excel
    End Function
    ''' <summary>
    ''' Esta clase permite escribir en un archivo activo de excel y da formato en especifico a la celda donde se esta escribiendo 
    ''' </summary>
    ''' <param name="ObjExcel"></param>
    ''' <param name="Fila"></param>
    ''' <param name="columna"></param>
    ''' <param name="Valor"></param>
    ''' <param name="Hoja"></param>
    ''' <param name="Nombre"></param>
    Public Sub EscribeExcelHojas(ByVal ObjExcel As Microsoft.Office.Interop.Excel.Application, ByVal Fila As Integer, ByVal columna As Integer, ByVal Valor As String, ByVal Hoja As Integer, Optional Nombre As String = "")
        Try
            If IsNumeric(Valor) Then
                If Valor.ToString.Substring(0, 0) = "0" Then
                    ObjExcel.Workbooks(1).Worksheets(Hoja).Cells(Fila, columna) = "'" & Valor
                Else
                    ObjExcel.Workbooks(1).Worksheets(Hoja).Cells(Fila, columna) = Convert.ToDecimal(Valor)
                    ObjExcel.Workbooks(1).Worksheets(Hoja).Columns(columna).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                End If
            Else
                ObjExcel.Workbooks(1).Worksheets(Hoja).Cells(Fila, columna) = Valor
            End If

            If Nombre <> "" Then
                ObjExcel.Workbooks(1).Worksheets(Hoja).Name = Nombre
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try


    End Sub
    ''' <summary>
    ''' Genera un nuevo número de Poliza para contabilizarlo 
    ''' </summary>
    ''' <param name="Id_Cliente"></param>
    ''' <param name="tipo"></param>
    ''' <param name="anio"></param>
    ''' <param name="mes"> De tipo string y con dos dígitos  </param>
    ''' <param name="tipo_poliza_sat"></param>
    ''' <returns></returns>
    Public Function Num_polizaS(ByVal Id_Cliente As Integer, ByVal tipo As Integer, ByVal anio As String, ByVal mes As String, ByVal tipo_poliza_sat As Integer)
        Dim Id_Poliza
        Dim consec As Integer
        Dim ds As DataSet = Eventos.Obtener_DS("SELECt max (consecutivo)+1 FROM Polizas WHERE Id_Empresa= " & Id_Cliente & " AND id_Anio = '" & anio & "' AND id_Mes ='" & mes & "' and Id_Tipo_Pol_Sat = " & tipo_poliza_sat & "")
        If IsDBNull(ds.Tables(0).Rows(0)(0)) = True Then
            consec = 1
        Else
            consec = ds.Tables(0).Rows(0)(0)
        End If
        Id_Poliza = "P" & Trim(Eventos.Obtener_DS("SELECT DISTINCT Tipo_Poliza.Clave_Tipo FROM Tipo_Poliza inner join Tipos_Poliza_Sat on Tipos_Poliza_Sat.Id_Tipo_poliza =Tipo_Poliza.Id_Tipo_poliza  where Tipos_Poliza_Sat.Id_Tipo_Pol_Sat= " & tipo & " ").Tables(0).Rows(0)(0)) & anio.Substring(2, 2) & mes & Id_Cliente & tipo_poliza_sat & "-" & consec
        Return Id_Poliza
    End Function
    ''' <summary>
    ''' Esta funcion permite devolver el nombre real del Usuario del Sistema 
    ''' </summary>
    ''' <param name="usr"></param>
    ''' <returns></returns>
    Public Function Usuario(ByVal usr) As String
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Try
            Return Eventos.Obtener_DS("SELECT Usuario FROM usuarios WHERE Usuario='" & usr & "'").Tables(0).Rows(0)(0)
        Catch ex As Exception
            RadMessageBox.Show("Error usuario no valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

        End Try

    End Function
    ''' <summary>
    ''' Esta clase actualiza los archivos XML para poder asignar la variable de complemento de pago en caso de ser contabilizado
    ''' </summary>
    ''' <param name="ID"> Este parametro recibe el Id_Registro_Xml de la DB</param>
    Public Sub Actualiza_Factura(ByVal ID As Integer)
        Dim sql As String = " UPDATE dbo.Xml_Sat SET Tiene_Comple = 1 WHERE Id_Registro_Xml = " & ID & " "
        If Eventos.Comando_sql(sql) > 0 Then
        End If
    End Sub
    Public Function Calcula_letraSat(ByVal Forma As String, ByVal uso As String)
        Dim Letra As String = ""
        Dim sql As String = "SELECT Tipo_Letra_SAT.clave AS Letras_Contabilidad, Forma_de_Pago.Clave AS FP ,Uso_CFDI.Clave AS Uso 
                            FROM Tipo_Letra_SAT INNER JOIN Uso_CFDI ON Uso_CFDI.Id_Uso_CFDI =Tipo_Letra_SAT.Id_Uso_CFDI 
                            INNER JOIN Forma_de_Pago ON Forma_de_Pago.Id_Forma_Pago =Tipo_Letra_SAT.Id_Forma_Pago WHERE Forma_de_Pago.Clave = '" & Trim(Forma) & "' AND Uso_CFDI.Clave = '" & uso & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Letra = Trim(ds.Tables(0).Rows(0)(0))
        Else
            Letra = "N/A"
        End If
        Return Letra
    End Function
    ''' <summary>
    ''' Carga el excel de cada complemento en un formato en especifico
    ''' </summary>
    ''' <param name="NomHoja"> El nombre de la hoja sea XML</param>
    ''' <returns></returns>
    Public Function CargarExcelXMLComplemento(Optional ByVal NomHoja As String = "") As DataSet
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Return Nothing
            End If
        End With

        Try
            Dim strconn As String, dt As New DataSet
            strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & archivo & ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'"
            Dim mconn As New OleDbConnection(strconn)
            Dim sql As String = "F1  AS Tipo,	F2  AS UUID,	F3  AS FechaEmision  ,F4  AS Folio,	F5  AS Serie, F6  AS	Subtot,	"
            sql &= " F7  AS Mone, F8  AS	Total, F9  AS	LugarExpedicion, F10  AS	RfcEmisor, F11  AS	NombreEmisor, F12  AS RegimenFiscal  , F13  AS	RfcReceptor,F14  AS	NombreReceptor,	"
            sql &= " F15 AS UsoCFDI,F16  AS	ClaveProdServ,F17  AS	Ca,F18 AS	Uni,F19  AS	Descripcion,	"
            sql &= " F20  AS ValorUnitario,  F21  AS	Imp,F22  AS	FechaDePago,F23  AS	FormaDePago,F24 AS	MoneP,  F25  AS	TipoCambioP,F26  AS	Mond,F27  AS	NumOperacion, F28  AS RfcEmisorCtaOrd,	"
            sql &= " F29  AS NomBancoOrdExt,F30  AS CtaOrdenante,F31  AS RfcEmisorCtaBen,F32  AS CtaBeneficiario,F33  AS TipoCadPago ,F34  AS CertPago,F35  AS CadPago,F36  AS SelloPago,F37  AS IdDocumento,F38  AS SerieDR,F39 AS FolioDR,"
            sql &= " F40  AS MonedaDR,F41  AS TipoCambioDR,F42  AS MetodoDePagoDR,F43  AS NumParcialidad,F44  AS ImpSaldoAnt ,F45  AS ImpPagado,F46  AS ImpSaldoInsoluto"
            Dim ad As New OleDbDataAdapter("Select " & sql & " from [" & IIf(NomHoja = "", "hoja1", NomHoja) & "$]", mconn)
            mconn.Open()
            ad.Fill(dt)
            mconn.Close()
            Return dt
        Catch ex As OleDbException
            RadMessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Esta funcion permite cargar el archivo excel que contiene todas las facturas y notas de credito emitidas o recibidas por el empresa 
    ''' </summary>
    ''' <param name="NomHoja"> El nombre de la Hoja debe ser XML </param>
    ''' <returns></returns>
    Public Function CargarExcelXMLSat(Optional ByVal NomHoja As String = "") As DataSet
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            '  .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Return Nothing
            End If
        End With
        'cargarlo en la datagrid
        Try
            Dim strconn As String, dt As New DataSet
            strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & archivo & ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'"
            Dim mconn As New OleDbConnection(strconn)
            Dim sql As String = "F1  AS Verificado_Asoc,	F2  AS Estado_SAT,	F3  AS Version  ,F4  AS Tipo,	F5  AS Fecha_Emision, F6  AS	Fecha_Timbrado,	"
            sql &= " F7  AS EstadoPago, F8  AS	FechaPago, F9  AS	Serie, F10  AS	Folio, F11  AS	UUID, F12  AS UUID_Relacion , F13  AS	RFC_Emisor,F14  AS	Nombre_Emisor,	"
            sql &= " F15 AS LugarDeExpedicion,F16  AS	RFC_Receptor,F17  AS	Nombre_Receptor,F18 AS	ResidenciaFiscal,F19  AS	NumRegIdTrib,	"
            sql &= " F20  AS UsoCFDI,  F21  AS	SubTotal,F22  AS	Descuento,F23  AS	Total_IEPS,F24 AS	IVA_16,  F25  AS	Retenido_IVA,F26  AS	Retenido_ISR,F27  AS	ISH, F28  AS Total,	"
            sql &= " F29  AS TotalOriginal, F30  AS	Total_Trasladados,F31  AS	Total_Retenidos,F32  AS	Total_LocalTrasladado,F33  AS	Total_LocalRetenido,	"
            sql &= " F34  AS Complemento,F35 AS	Moneda,F36  AS	Tipo_De_Cambio,F37  AS	FormaDePago,F38  AS	Metodo_de_Pago,F39  AS	NumCtaPago,F40  AS	Condicion_de_Pago,	"
            sql &= " F41  AS Conceptos,F42  AS	Combustible,F43  AS	IEPS_3,F44 AS	IEPS_6,F45  AS	IEPS_7,F46 AS	IEPS_8,F47  AS	IEPS_9,F48 AS	IEPS_26,F49  AS	IEPS_30,F50  AS	IEPS_53,F51  AS	IEPS_160,	"
            sql &= " F52  AS Archivo_XML,F53  AS	Direccion_Emisor,F54 AS	Localidad_Emisor,F55  AS	Direccion_Receptor,F56 AS	Localidad_Receptor,F57 AS	IVA_8,F58 as IEPS_301,F59 as Iva6 "
            Dim ad As New OleDbDataAdapter("Select " & sql & " from [" & IIf(NomHoja = "", "hoja1", NomHoja) & "$]", mconn)
            mconn.Open()
            ad.Fill(dt)
            mconn.Close()
            Return dt
        Catch ex As OleDbException
            RadMessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Esta funcion permite retornar el mes en letra a partir de un string con número 
    ''' </summary>
    ''' <param name="Mes"> el mes debe ser de tipo string ejemplos "01" , "12" </param>
    ''' <returns>  </returns>
    Public Function MesEnletra(ByVal Mes As String) As String
        Select Case Mes
            Case "01"
                Return "Enero"
            Case "02"
                Return "Febrero"
            Case "03"
                Return "Marzo"
            Case "04"
                Return "Abril"
            Case "05"
                Return "Mayo"
            Case "06"
                Return "Junio"
            Case "07"
                Return "Julio"
            Case "08"
                Return "Agosto"
            Case "09"
                Return "Septiembre"
            Case "10"
                Return "Octubre"
            Case "11"
                Return "Noviembre"
            Case "12"
                Return "Diciembre"
            Case Else
                Return ""
        End Select
    End Function
    ''' <summary>
    ''' Esta funcion devuelve un Objeto con el Path de la boveda de archivos XML de un Usuario del sistema
    ''' </summary>
    ''' <param name="Usr"></param>
    ''' <returns></returns>
    Public Function Boveda(ByVal Usr As String)
        Dim Bov As String = ""
        Dim Sql As String = " SELECT Boveda from Usuarios WHERE Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%'"
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If IsDBNull(ds.Tables(0).Rows(0)(0)) = True Then
                Bov = ""
            Else
                Bov = ds.Tables(0).Rows(0)(0)
            End If
        Else
            Bov = ""
        End If

        Return Bov
    End Function
    ''' <summary>
    ''' Esta fucnion retorna un dataset que contiene todos los nodos de un archivo XML
    ''' </summary>
    ''' <param name="Archivo"> Valor opcional </param>
    ''' <returns></returns>
    Public Function CargarXMLaDataSet(Optional ByVal Archivo As String = "") As DataSet
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim ds As New DataSet
        If Archivo = "" Then
            Dim dialogo As New OpenFileDialog
            With dialogo
                .Title = "Seleccione el archivo XML para cargar..."
                .Filter = "Archivos MXL (*.xml)|*.xml"
                If .ShowDialog() = DialogResult.OK Then
                    Archivo = .FileName
                Else
                    Return ds
                End If
            End With
        End If
        Try
            ds.ReadXml(Archivo)
            Return ds
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

            Return ds
        End Try
    End Function
    ''' <summary>
    ''' Esta Clase permite ubicar un archivo en una carpeta y asignarle el foco en el Explorador de Windows
    ''' </summary>
    ''' <param name="Carpeta"> Path donde se debera buscar el Archivo </param>
    ''' <param name="Archivo"> Nombre del archivo con Extension </param>
    Public Sub Abrir_Capeta_con_archivo(ByVal Carpeta As String, ByVal Archivo As String)

        Dim WScript As Object
        Dim cadena As String = "%windir%\explorer.exe /select,"
        WScript = CreateObject("WScript.Shell")
        WScript.Run(cadena & Chr(34) & Carpeta & "\" & Archivo & Chr(34))
        WScript = Nothing
    End Sub
    ''' <summary>
    ''' Esta funcion permite obtener informacion de la base de datos Tusa
    ''' </summary>
    ''' <param name="sql"> Cadena con la query a inyectar </param>
    ''' <param name="server"> Ip o instacia de consulta </param>
    ''' <param name="user"> Usuario de tipo Login de la DB</param>
    ''' <param name="pass"> Passwor de Login </param>
    ''' <returns></returns>
    Public Function Obtener_DSTusa(ByVal sql As String, Optional ByVal server As String = "2.tcp.ngrok.io,13130", Optional ByVal user As String = "tusasa", Optional ByVal pass As String = "51s73m452019") As DataSet
        Dim source As String = "Data Source=tcp:" & Trim(server) & ";Persist Security Info=True;User ID=" & user & ";password=" & pass & ";Initial Catalog=tusa"
        Dim Conn As New SqlConnection(source)
        Dim dA1 As New SqlDataAdapter(sql, Conn)
        Dim dS1 As New DataSet
        Try
            Conn.Open()
            'Dim dA1 As New SqlDataAdapter(sql, Conn)
            'Dim dS1 As New DataSet
            dA1.Fill(dS1, "Temp")
            Conn.Close()
            Return dS1
        Catch ex As Exception
            'Auditoria sistemas
            RadMessageBox.Show("Error: " & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Return dS1
        End Try

    End Function
    ''' <summary>
    ''' Esta clase permite darle diseño a un DataGridView
    ''' </summary>
    ''' <param name="TABLA"> El nombre del DataGridView </param>
    ''' <param name="Centrar"> Indica si el los encabezados seran centrados </param>
    Public Sub DiseñoTabla(ByVal TABLA As DataGridView, Optional Centrar As Boolean = False)
        TABLA.BackColor = Color.CadetBlue
        TABLA.BorderStyle = BorderStyle.Fixed3D
        TABLA.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        TABLA.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Regular)
        TABLA.RowsDefaultCellStyle.SelectionBackColor = Color.Crimson
        TABLA.GridColor = Color.SteelBlue
        TABLA.EnableHeadersVisualStyles = False
        TABLA.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue
        TABLA.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke
        TABLA.ColumnHeadersDefaultCellStyle.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Regular)
        TABLA.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        TABLA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        TABLA.ColumnHeadersHeight = 30
        TABLA.RowHeadersDefaultCellStyle.BackColor = Color.CadetBlue
        TABLA.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Crimson
        TABLA.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        TABLA.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
        'TABLA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        TABLA.AllowUserToAddRows = False
        TABLA.ScrollBars = ScrollBars.Both


        If Centrar = True Then
            For Each Col In TABLA.Columns
                TABLA.Columns(Col.index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

        End If
    End Sub
    ''' <summary>
    ''' Esta clase permite darle diseño a un DataGridView y permite dar color a las columnas deseadas por el usuario
    ''' </summary>
    ''' <param name="TABLA"> El nombre del DataGridView </param>
    ''' <param name="Centrar"> Indica si el los encabezados seran centrados </param>
    Public Sub DiseñoTablaEnca(ByVal TABLA As DataGridView, Optional Centrar As Boolean = False)
        TABLA.BackColor = Color.CadetBlue
        TABLA.BorderStyle = BorderStyle.Fixed3D
        TABLA.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Regular)
        TABLA.RowsDefaultCellStyle.SelectionBackColor = Color.Crimson
        TABLA.GridColor = Color.SteelBlue
        TABLA.EnableHeadersVisualStyles = False
        TABLA.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue
        TABLA.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke
        TABLA.ColumnHeadersDefaultCellStyle.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Regular)
        TABLA.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        TABLA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        TABLA.ColumnHeadersHeight = 30
        TABLA.RowHeadersDefaultCellStyle.BackColor = Color.CadetBlue
        TABLA.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Crimson
        TABLA.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        TABLA.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
        'TABLA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        TABLA.AllowUserToAddRows = False
        TABLA.ScrollBars = ScrollBars.Both
        If Centrar = True Then
            For Each Col In TABLA.Columns
                TABLA.Columns(Col.index).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

        End If
    End Sub
    ''' <summary>
    ''' Esta funcion devuelve un decimal con el saldo inicial de la cuanta especificada 
    ''' </summary>
    ''' <param name="Cuenta"> Tipo string y con un largo fijo de 16 caracteres expresados  ################ </param>
    ''' <param name="fecha"> De tipo dd/MM/yyyy </param>
    ''' <param name="id_cliente"> Empresa de Consulta </param>
    ''' <returns></returns>
    Public Function Calcula_Saldos_Iniciales(ByVal Cuenta As String, ByVal fecha As String, ByVal id_cliente As Integer)
        Dim Saldo As Decimal = 0
        Dim where As String = ""
        If Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(4, 4) = "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' "
        ElseIf Cuenta.Substring(4, 4) >= "0000" And Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "'  "
        ElseIf Cuenta.Substring(4, 4) > "0000" And Cuenta.Substring(8, 4) > "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "' and Catalogo_de_Cuentas.Nivel3 =  '" & Cuenta.Substring(8, 4) & "'  "
        ElseIf Cuenta.Substring(4, 4) > "0000" And Cuenta.Substring(8, 4) > "0000" And Cuenta.Substring(12, 4) > "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "' and Catalogo_de_Cuentas.Nivel3 =  '" & Cuenta.Substring(8, 4) & "' and Catalogo_de_Cuentas.Nivel4 =  '" & Cuenta.Substring(12, 4) & "'  "
        End If


        Dim sql As String = " SELECT  CASE WHEN Naturaleza = 'D' THEN sUM(cargos - abonos) WHEN Naturaleza = 'A' THEN Sum(abonos - cargos) END AS Saldo, Naturaleza   FROM (
                              SELECT Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza,sum(cargo ) AS Cargos,sum (abono )AS Abonos FROM Detalle_Polizas 
                              INNER JOIN Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza
                              INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                              INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
                              WHERE Polizas.Aplicar_Poliza =1 AND Polizas.Id_Empresa= " & id_cliente & " AND  " & where & " AND   Polizas.Concepto = 'Poliza Cierre' AND  Polizas.Id_Anio ='" & fecha & "'
                              GROUP BY Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza
                              ) AS Tabla_Saldos GROUP BY NATURALEZA"

        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Saldo = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
        Else
            Saldo = 0
        End If
        Return Saldo

    End Function
    ''' <summary>
    ''' Esta clase permite verificar todas las cuentas con saldo de una empresa 
    ''' </summary>
    ''' <param name="Anio"></param>
    ''' <param name="Id_Cliente"></param>
    Public Sub Cuentas(ByVal Anio As Integer, ByVal Id_Cliente As Integer)
        Dim Sql As String = "SELECT DISTINCT  Detalle_Polizas.Cuenta  FROM Polizas INNER JOIN Detalle_Polizas ON Detalle_Polizas.ID_poliza = Polizas.ID_poliza 
                WHERE  Id_Empresa = " & Id_Cliente & " AND ID_anio = " & Anio & " OR 
            POLIZAS.ID_poliza IN (SELECT Polizas.ID_poliza  FROM Polizas 
                WHERE Concepto = 'Poliza Cierre' AND ID_anio = " & Anio - 1 & " AND Id_Empresa = " & Id_Cliente & ")"
        Dim Datos As DataSet = Eventos.Obtener_DS(Sql)
        If Datos.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To Datos.Tables(0).Rows.Count - 1
                Eventos.Inserta_cuenta(Datos.Tables(0).Rows(i)("Cuenta"), Id_Cliente, Anio)
            Next
        End If
    End Sub
    ''' <summary>
    ''' Esta funcion permite cargar el excel con el catalogo de cuentas de una empresa 
    ''' </summary>
    ''' <param name="NomHoja"> Es una constante llamada Catalogo </param>
    ''' <returns></returns>
    Public Function CargarExcelCatN(Optional ByVal NomHoja As String = "") As DataSet
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel del Catalogo de Cuentas"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            '  .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Return Nothing
            End If
        End With
        'cargarlo en la datagrid
        Try
            Dim strconn As String, dt As New DataSet
            strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & archivo & ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'"
            Dim mconn As New OleDbConnection(strconn)

            Dim sql As String = "F1  AS Nivel1, F2  AS Nivel2,F3  AS Nivel3,F4  AS Nivel4,F5  AS [Cuenta Completa], F6  AS Descripcion,F7  AS Naturaleza,F8  AS Clasificacion,F9  AS Codigo_Agrupador,F10  AS RFC,F11  AS Balanza,F12  AS Cta_ceros,F13  AS Cta_Cargo_Cero,F14  AS Cta_Abono_Cero,F15  AS Balance,F16  AS Estado_de_Resultados,F17  AS CuentaEnlace"

            Dim ad As New OleDbDataAdapter("Select " & sql & " from [" & IIf(NomHoja = "", "hoja1", NomHoja) & "$]", mconn)
            mconn.Open()
            ad.Fill(dt)
            mconn.Close()
            Return dt
        Catch ex As OleDbException
            RadMessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Esta clase llena un DataGridView con la informacion contenida en la funcion CargarExcelCatN
    ''' </summary>
    ''' <param name="ds"> la variable con la funcion CargarExcelCatN </param>
    ''' <param name="Tabla"> El DataGridView que almacenara el catalogo </param>
    Public Sub LlenarDataGrid_DSCatalogo(ByVal ds As DataSet, ByVal Tabla As Windows.Forms.DataGridView)
        Try
            Tabla.DataSource = ds.Tables(0).DefaultView
        Catch ex As Exception

        End Try

    End Sub
    Public Class Ctas_S
        Public Property Int As Integer
        Public Property Cta As String
    End Class
    ''' <summary>
    ''' Esta clase permite insertar en la tabla Cuentas_Con_Saldo las cuentas con saldo asi como el año en el que generaron movimientos, evalua si la cuenta tines subcuentas (Cuentas Hijas)
    ''' </summary>
    ''' <param name="Cuenta"></param>
    ''' <param name="Clt"></param>
    ''' <param name="Anio"></param>
    Private Sub Inserta_cuenta(ByVal Cuenta As String, ByVal Clt As Integer, ByVal Anio As Integer)
        Dim Ctas As New List(Of Ctas_S)
        Dim Contador As Integer
        Dim Consulta As String = ""
        Dim DS As DataSet
        If Cuenta.Substring(12, 4) = "0000" And Cuenta.Substring(8, 4) > "0000" Then ' Madre
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '0000'  And Id_Empresa = " & Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '" & Cuenta.Substring(4, 4) & "'  And Id_Empresa = " & Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Ctas.Add(New Ctas_S() With {.Cta = Cuenta, .Int = Contador})
        ElseIf Cuenta.Substring(12, 4) > "0000" And Cuenta.Substring(8, 4) > "0000" Then 'Hija

            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '0000'  And Id_Empresa = " & Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '" & Cuenta.Substring(4, 4) & "'  And Id_Empresa = " & Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '" & Cuenta.Substring(4, 4) & "' AND Nivel3 = '" & Cuenta.Substring(8, 4) & "'  And Id_Empresa = " & Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Ctas.Add(New Ctas_S() With {.Cta = Cuenta, .Int = Contador})

        ElseIf Cuenta.Substring(4, 4) > "0000" And Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(12, 4) = "0000" Then 'Primer Nivel
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '0000'  And Id_Empresa = " & Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Ctas.Add(New Ctas_S() With {.Cta = Cuenta, .Int = Contador})
        Else
            Ctas.Add(New Ctas_S() With {.Cta = Cuenta, .Int = Contador})
        End If


        For Each It In Ctas
            If Eventos.ObtenerValorDB("Cuentas_Con_Saldo", "Cuenta", "  cuenta = " & It.Cta & "  and Id_Empresa =" & Clt & "  and anio= " & Anio & " ", True) = " " Then
                Consulta = "INSERT INTO dbo.Cuentas_Con_Saldo	("
                Consulta &= " 	Cuenta,"
                Consulta &= " 	Usar,"
                Consulta &= " 	Anio,"
                Consulta &= " 	Id_Empresa"
                Consulta &= " 	)"
                Consulta &= " VALUES "
                Consulta &= " 	("
                Consulta &= " 	" & It.Cta & ","
                Consulta &= " 	1,"
                Consulta &= " 	" & Anio & ","
                Consulta &= " 	1"
                Consulta &= " 	)"
                If Eventos.Comando_sql(Consulta) = 0 Then

                End If
            End If
        Next

    End Sub
    Public Function Calcula_Saldos_InicialesP(ByVal Cuenta As String, ByVal Anio As String, ByVal id_cliente As Integer, ByVal Periodo As String)
        Dim Saldo As Decimal = 0
        Dim where As String = ""
        If Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(4, 4) = "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' "
        ElseIf Cuenta.Substring(4, 4) >= "0000" And Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "'  "
        ElseIf Cuenta.Substring(4, 4) > "0000" And Cuenta.Substring(8, 4) > "0000" And Cuenta.Substring(12, 4) = "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "' and Catalogo_de_Cuentas.Nivel3 =  '" & Cuenta.Substring(8, 4) & "'  "
        ElseIf Cuenta.Substring(4, 4) > "0000" And Cuenta.Substring(8, 4) > "0000" And Cuenta.Substring(12, 4) > "0000" Then
            where = "Catalogo_de_Cuentas.Nivel1 =  '" & Cuenta.Substring(0, 4) & "' and Catalogo_de_Cuentas.Nivel2 =  '" & Cuenta.Substring(4, 4) & "' and Catalogo_de_Cuentas.Nivel3 =  '" & Cuenta.Substring(8, 4) & "' and Catalogo_de_Cuentas.Nivel4 =  '" & Cuenta.Substring(12, 4) & "'  "
        End If

        Dim sql As String = " SELECT  CASE WHEN Naturaleza = 'D' THEN sUM(cargos - abonos) WHEN Naturaleza = 'A' THEN Sum(abonos - cargos) END AS Saldo   FROM (
                              SELECT Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza,sum(cargo ) AS Cargos,sum (abono )AS Abonos FROM Detalle_Polizas 
                              INNER JOIN Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza
                              INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                              INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
                              WHERE Polizas.Aplicar_Poliza =1 and Polizas.Id_Empresa= " & id_cliente & " AND  " & where & "  AND  Polizas.Id_Anio ='" & Anio & "'  AND " & Periodo & "
                              GROUP BY Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza
                              ) AS Tabla_Saldos GROUP BY NATURALEZA"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Saldo = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
        Else
            Saldo = 0
        End If
        Return Saldo

    End Function
    ''' <summary>
    ''' Esta funcion retorna un decimal con la utilidad Bruta de una Empresa
    ''' </summary>
    ''' <param name="id_cliente"></param>
    ''' <param name="Fi"> Fecha inicial de Consulta</param>
    ''' <param name="Ff"> Fecha final de Consulta </param>
    ''' <returns></returns>
    Public Function Utilidad_Bruta(ByVal id_cliente As Integer, ByVal Fi As String, ByVal Ff As String)
        Dim saldo As Decimal = 0

        Dim sql As String = "  SELECT sum(Saldo) AS S , Naturaleza FROM ("
        sql &= " SELECT   Case WHEN Naturaleza = 'D' THEN sum(cargo)-sum(abono) WHEN Naturaleza = 'A' THEN sum(abono)-sum(cargo) END AS Saldo ,"
        sql &= " Catalogo_de_Cuentas.Naturaleza  FROM Detalle_Polizas"
        sql &= " INNER JOIN  Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza"
        sql &= " INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta"
        sql &= " WHERE Catalogo_de_Cuentas.Id_Empresa = " & id_cliente & "   and Detalle_Polizas.Cuenta  IN ( SELECT Catalogo_de_Cuentas.Cuenta  FROM Catalogo_de_Cuentas WHERE Catalogo_de_Cuentas.Clasificacion "
        sql &= " IN ('IVS', 'CVE','GGE','GVE','OGS','GFI') AND Catalogo_de_Cuentas.Id_Empresa = " & id_cliente & " )"
        sql &= " And Polizas.Aplicar_Poliza = 1 And Polizas.Id_Empresa = " & id_cliente & " And (Polizas.Fecha_captura >= " & Eventos.Sql_hoy(Fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(Ff) & ")"
        sql &= " GROUP BY  Catalogo_de_Cuentas.Naturaleza  ) AS Tabla GROUP BY  Naturaleza "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        Try
            If ds.Tables(0).Rows.Count > 0 Then
                Try
                    saldo = ds.Tables(0).Rows(1)(0)
                Catch ex As Exception
                    saldo = 0
                End Try
                saldo = ds.Tables(0).Rows(0)(0) - saldo
            Else
                saldo = 0
            End If
        Catch ex As Exception
            saldo = 0
        End Try

        Dim saldos As String = "SELECT   Detalle_Polizas.cargo,Detalle_Polizas.Abono 
                                FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
                                WHERE Polizas.Aplicar_Poliza =1 AND Polizas.Concepto = 'Poliza Cierre' AND (Clasificacion IN ('IVS', 'CVE','GGE','GVE','OGS','GFI')) AND (Nivel1 > 0) AND   ( polizas.ID_anio =" & Fi.Substring(6, 4) - 1 & " ) and Polizas.Id_Empresa= " & id_cliente & "
                                GROUP BY Polizas.ID_anio,Polizas.ID_mes,Catalogo_de_Cuentas.Naturaleza , Detalle_Polizas.cargo  ,  Detalle_Polizas.abono  "

        Dim sal As Decimal = 0
        ds.Clear()

        ds = Eventos.Obtener_DS(saldos)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i)(0) <> 0 Then
                    sal += ds.Tables(0).Rows(i)(0)
                Else
                    sal += ds.Tables(0).Rows(i)(1)
                End If
            Next
        Else
            sal = 0
        End If
        Return (sal + saldo)
    End Function
    Public Function Num_poliza(ByVal Id_Cliente As Integer, ByVal tipo As Integer, ByVal anio As String, ByVal mes As String, ByVal tipo_poliza_sat As Integer)
        Dim Id_Poliza = ""
        Dim consec As Integer
        Dim ds As DataSet = Eventos.Obtener_DS("SELECt max (consecutivo)+1 FROM Polizas WHERE Id_Empresa= " & Id_Cliente & " AND id_Anio = '" & anio & "' AND id_Mes ='" & mes & "' and Id_Tipo_Pol_Sat = " & tipo_poliza_sat & "")
        If IsDBNull(ds.Tables(0).Rows(0)(0)) = True Then
            consec = 1
        Else
            consec = ds.Tables(0).Rows(0)(0)
        End If
        Id_Poliza = "P" & Trim(Eventos.Obtener_DS("SELECT DISTINCT Tipo_Poliza.Clave_Tipo FROM Tipo_Poliza inner join Tipos_Poliza_Sat on Tipos_Poliza_Sat.Id_Tipo_poliza =Tipo_Poliza.Id_Tipo_poliza  where Tipos_Poliza_Sat.Id_Tipo_Pol_Sat= " & tipo & " ").Tables(0).Rows(0)(0)) & anio.Substring(2, 2) & mes & Id_Cliente & tipo_poliza_sat & "-" & consec
        Return Id_Poliza
    End Function
    ''' <summary>
    ''' Esta funcion permite cargar un archivo excel con los saldos iniciales de cada cuenta de ultimo nivel
    ''' </summary>
    ''' <param name="NomHoja"> El nombre de la hoja debe ser Saldos </param>
    ''' <returns></returns>
    Public Function CargarExcelXMLCatSaldosIniciales(Optional ByVal NomHoja As String = "") As DataSet
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            '.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Return Nothing
            End If
        End With
        'cargarlo en la datagrid
        Try
            Dim strconn As String, dt As New DataSet
            strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & archivo & ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'"
            Dim mconn As New OleDbConnection(strconn)
            Dim sql As String = "F1  AS Cuenta_Catalogo,F2  AS Descripcion,	F3  AS Nat, F4  AS Cta, F5  AS Saldo"
            Dim ad As New OleDbDataAdapter("Select " & sql & " from [" & IIf(NomHoja = "", "hoja1", NomHoja) & "$]", mconn)
            mconn.Open()
            ad.Fill(dt)
            mconn.Close()
            Return dt
        Catch ex As OleDbException
            RadMessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Genera un reporte en plantilla mensual de la Balanza de comprobacion
    ''' </summary>
    ''' <param name="plantilla"></param>
    ''' <param name="visible"></param>
    ''' <param name="empresa"></param>
    ''' <returns></returns>
    Public Function NuevoExcelMeses(ByVal plantilla As String, ByVal visible As Boolean, Optional ByVal empresa As Integer = 1) As Microsoft.Office.Interop.Excel.Application
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim m_Excel As Microsoft.Office.Interop.Excel.Application
        Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook
        Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet
        Dim archivo As String
        If empresa = 0 Or empresa = 1 Then
            archivo = Application.StartupPath & IIf(empresa = 1, "\Plantillas\Contable", "\Plantillas") & plantilla & ".xlsx"
        Else
            archivo = Application.StartupPath & IIf(My.Forms.Inicio.TxtEmpresa.Text = "Autotransortacion Mexicana ATM", "\Plantillas", "\Plantillas") & plantilla & ".xlsx"
        End If

        Try
            m_Excel = New Microsoft.Office.Interop.Excel.Application
            m_Excel.Visible = False
            objLibroExcel = m_Excel.Workbooks.Add(archivo)
            For i As Integer = 1 To 13
                objHojaExcel = objLibroExcel.Worksheets.Add()
            Next
            objHojaExcel = objLibroExcel.Worksheets(1)
            objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            objHojaExcel.Activate()
            m_Excel.Visible = visible
        Catch ex As Exception
            RadMessageBox.Show("Error al crear el archivo de Excel: " & ex.Message)
            m_Excel = Nothing
        End Try
        Return m_Excel
    End Function
    ''' <summary>
    ''' Abre el Explorador de Windows en la carpeta especifica 
    ''' </summary>
    ''' <param name="carpeta"></param>
    Public Sub Abrir_Capeta(ByVal carpeta As String)
        Process.Start("explorer.exe", carpeta)
    End Sub
    Public Function NuevoExcelM(ByVal plantilla As String, ByVal visible As Boolean, Optional ByVal empresa As Integer = 1) As Microsoft.Office.Interop.Excel.Application
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim m_Excel As Microsoft.Office.Interop.Excel.Application
        Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook
        Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet
        Dim archivo As String
        If empresa = 0 Or empresa = 1 Then
            archivo = Application.StartupPath & IIf(empresa = 1, "\Plantillas\Contable", "\Plantillas") & plantilla & ".xlsm"
        Else
            archivo = Application.StartupPath & IIf(My.Forms.Inicio.TxtEmpresa.Text = "Autotransportacion Mexicana ATM", "\Plantillas", "\Plantillas") & plantilla & ".xlsm"
        End If

        Try
            m_Excel = New Microsoft.Office.Interop.Excel.Application
            m_Excel.Visible = False
            objLibroExcel = m_Excel.Workbooks.Add(archivo)
            objHojaExcel = objLibroExcel.Worksheets(1)
            objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            objHojaExcel.Activate()
            m_Excel.Visible = visible
        Catch ex As Exception
            RadMessageBox.Show("Error al crear el archivo de Excel: " & ex.Message)
            m_Excel = Nothing
        End Try
        Return m_Excel
    End Function
    ''' <summary>
    ''' Exporta un archico excel de manera masiva utilizando el Clipboard
    ''' </summary>
    ''' <param name="HojaExcel"></param>
    ''' <param name="dataObj"></param>
    ''' <param name="Hoja"></param>
    ''' <param name="Nombre"></param>
    ''' <param name="NomHoja"></param>
    Public Sub ExMasivo(ByVal HojaExcel As Excel.Application, ByVal dataObj As DataObject, ByVal Hoja As Integer, ByVal Nombre As String, ByVal NomHoja As String)

        Dim H As Excel.Worksheet
        HojaExcel.Visible = False
        Dim Rango As Excel.Range
        Try
            Clipboard.SetDataObject(dataObj)
            HojaExcel.Workbooks(1).Worksheets(Hoja).Name = NomHoja
            H = HojaExcel.Worksheets(Hoja)
            H.Activate()
            HojaExcel.Range(Nombre).Select()
            HojaExcel.ActiveSheet.Paste
            With HojaExcel
                .Range(Nombre).Select()
                .Selection.EntireColumn.Delete
            End With
        Catch ex As Exception
            H = Nothing
            HojaExcel = Nothing
        End Try
        H = Nothing
        HojaExcel = Nothing

    End Sub
    ''' <summary>
    ''' Ajusta las filas mediante WrapMode 
    ''' </summary>
    ''' <param name="Columna"></param>
    ''' <param name="Tabla"></param>
    Public Sub AltoFilas(ByVal Columna As Integer, ByVal Tabla As DataGridView)
        Tabla.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        Dim col As DataGridViewColumn = Tabla.Columns(Columna)
        col.DefaultCellStyle.WrapMode = DataGridViewTriState.True
    End Sub
    ''' <summary>
    ''' Obtiene los dias del cada mes
    ''' </summary>
    ''' <param name="Mes"></param>
    ''' <returns></returns>
    Public Function Diasdelmes(ByVal Mes As String)
        Dim Hacer As String
        Dim sql As String = " SELECT dbo.fn_DaysOfMonth('" & Mes & "') AS Dias "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Hacer = ds.Tables(0).Rows(0)(0)
        Else
            Hacer = 0
        End If
        Return Hacer
    End Function
    ''' <summary>
    ''' Devuelve el mes en numero a partir de un string
    ''' </summary>
    ''' <param name="Mes"></param>
    ''' <returns></returns>
    Public Function MesEnNumero(ByVal Mes As String) As String
        Select Case Mes
            Case "Enero"
                Return "01"
            Case "Febrero"
                Return "02"
            Case "Marzo"
                Return "03"
            Case "Abril"
                Return "04"
            Case "Mayo"
                Return "05"
            Case "Junio"
                Return "06"
            Case "Julio"
                Return "07"
            Case "Agosto"
                Return "08"
            Case "Septiembre"
                Return "09"
            Case "Octubre"
                Return "10"
            Case "Noviembre"
                Return "11"
            Case "Diciembre"
                Return "12"
            Case Else
                Return ""
        End Select
    End Function
    ''' <summary>
    ''' Importa un archivo de tipo excel de Plantilla para Cargar el personal para Generar Movimientos ante el IMSS
    ''' </summary>
    ''' <param name="NomHoja"> La Hoja debe llamarce Datos</param>
    ''' <returns></returns>
    Public Function CargarExcelXMLDatosPersonalClientes(Optional ByVal NomHoja As String = "") As DataSet
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Return Nothing
            End If
        End With
        'cargarlo en la datagrid
        Try
            Dim strconn As String, dt As New DataSet
            strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & archivo & ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'"
            Dim mconn As New OleDbConnection(strconn)

            Dim sql As String = "F1  AS ID_matricula ,F2  AS Ap_paterno,	F3  AS Ap_materno  ,F4  AS Nombres,	 F5  AS	Registro_Patronal,	"
            sql &= " F6  AS Dig_Verificador_RP, F7  AS	No_IMSS, F8  AS	Dig_Verif_IMSS, F9  AS	Salario_Base, F10  AS	Tipo_Trabajador, F11  AS Tipo_Salario , F12  AS Sem_Jornada_Reducida , F13  AS	Unidad_Medicina_Familiar,	"
            sql &= " F14 AS Guia,F15  AS CURP,F16  AS	Fecha_alta "
            Dim ad As New OleDbDataAdapter("Select " & sql & " from [" & IIf(NomHoja = "", "hoja1", NomHoja) & "$]", mconn)
            mconn.Open()
            ad.Fill(dt)
            mconn.Close()
            Return dt
        Catch ex As OleDbException
            RadMessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Clase para generar polizas para Gastos 
    ''' </summary>
    Public Class Contabilizar_XML
        Public Property UUID As String
        Public Property Orden As String
        Public Property Poliza As String
        Public Property Importe_Grabado As Decimal
        Public Property Importe_Exento As Decimal
        Public Property Total As Decimal
        Public Property Iva As Decimal
        Public Property Tasa As Decimal
        Public Property Estatus As String
        Public Property Serie As String
        Public Property Fecha As String
        Public Property Emisor As String
        Public Property Nombre As String
        Public Property Nombres_Deudor As String
        Public Property Matricula As String
        Public Property RFC_Deudor As String
        Public Property Metodo As String
    End Class
    ''' <summary>
    ''' Evento para crear un nuevo Id de poliza
    ''' </summary>
    ''' <param name="Mes"></param>
    ''' <param name="Anio"></param>
    ''' <param name="Empresa"></param>
    ''' <param name="Tipo"></param>
    ''' <returns></returns>
    Public Function Calcula_poliza(ByVal Mes As String, ByVal Anio As String, ByVal Empresa As Integer, ByVal Tipo As String) As String
        Dim poliza As String = Num_polizaS(Empresa, Tipo, Anio, Mes, Tipo)
        Calcula_poliza = poliza
        Return Calcula_poliza
    End Function

    ''' <summary>
    ''' Genera un nuevo registro en la BD de Polizas
    ''' </summary>
    ''' <param name="id_poliza"></param>
    ''' <param name="anio"></param>
    ''' <param name="mes"></param>
    ''' <param name="dia"></param>
    ''' <param name="consecutivo"></param>
    ''' <param name="tipo"></param>
    ''' <param name="fecha"></param>
    ''' <param name="concepto"></param>
    ''' <param name="movimiento"></param>
    ''' <param name="num_pol"></param>
    ''' <param name="registro"></param>
    ''' <param name="UUID"></param>
    ''' <param name="OT"></param>
    ''' <returns></returns>
    Public Function Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String, ByVal num_pol As Integer, ByVal registro As Integer,
                               Optional ByVal UUID As String = "", Optional ByVal OT As String = "") As Boolean

        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	ID_poliza,      "
        sql &= "     ID_anio,        "
        sql &= "     ID_mes,        "
        sql &= "     ID_dia,        "
        sql &= "     consecutivo,    "
        sql &= "     Num_Pol,    "
        sql &= "     Id_Tipo_Pol_Sat,"
        sql &= "     Fecha,          "
        sql &= "     Concepto,      "
        sql &= "     ID_Empresa,     "
        sql &= "     No_Mov,        "
        sql &= "     Fecha_captura,  "
        sql &= "     Movto,         "
        sql &= "     Usuario,Aplicar_Poliza         "
        sql &= " 	)               "
        sql &= " VALUES              "
        sql &= " 	(               "
        sql &= " 	'" & id_poliza & "'," '@id_poliza,         
        sql &= " 	" & anio & "," '@id_anio,           
        sql &= " 	'" & mes & "'," '@id_mes,     
        sql &= " 	'" & dia & "'," '@id_dia,     
        sql &= " 	" & consecutivo & "," '@consecutivo,   
        sql &= " 	" & num_pol & "," '@num_pol,  
        sql &= " 	" & tipo & "," '@id_tipo_poliza, 
        sql &= " 	" & Eventos.Sql_hoy(fecha) & "," '@fecha,             
        sql &= " 	'" & concepto & "'," '@concepto,          
        sql &= " 	1," '@ID_Empresa,        
        sql &= " 	'" & movimiento & "'," '@no_mov,            
        sql &= " 	" & Eventos.Sql_hoy("" & dia & "/" & mes & "/" & anio & "") & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & UCase(My.Forms.Inicio.LblUsuario.Text.Trim()) & "', 1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            Creapoliza = True
            Actualiza_Registro(id_poliza, registro)
            Actualiza_RegistroXMLAuditados(id_poliza, UUID, OT)
        Else
            Creapoliza = False
        End If
        Return Creapoliza
    End Function
    ''' <summary>
    ''' Una vez que se crea una nueva poliza ligada a un XML el sistema actualiza la Tabla xml_sat para ligar la polzia al registro
    ''' </summary>
    ''' <param name="poliza"></param>
    ''' <param name="registro"></param>
    Public Sub Actualiza_Registro(ByVal poliza As String, ByVal registro As Integer)
        Dim sql As String = " UPDATE dbo.xml_sat SET ID_poliza = '" & poliza & "' WHERE Id_Registro_Xml = " & registro & "  "
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    ''' <summary>
    ''' Actualiza el registro de la tabla XmlAuditados con la poliza, Orden de Traslado correspondiente dependiendo del UUID
    ''' </summary>
    ''' <param name="poliza"></param>
    ''' <param name="UUID"></param>
    ''' <param name="Orden"></param>
    Public Sub Actualiza_RegistroXMLAuditados(ByVal poliza As String, ByVal UUID As String, ByVal Orden As String)
        Dim sql As String = " UPDATE dbo.XmlAuditados SET Id_PolizaS = '" & poliza & "' , Orden ='" & Orden & "'WHERE UUID = '" & UUID & "'  "
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    ''' <summary>
    ''' Busca la Factura correspondiente al UUID Solicitado
    ''' </summary>
    ''' <param name="Folio_Fiscal"></param>
    ''' <param name="detaclle"></param>
    ''' <returns></returns>
    Public Function Buscafactura(ByVal Folio_Fiscal As String, ByVal detaclle As String)
        Dim hacer As Boolean
        Dim sql As String = "select * from Facturas where Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    ''' <summary>
    ''' Genera los registros contables de cada UUID de acuerdo al porcenteje que se ocupo del UUID encada OT
    ''' </summary>
    ''' <param name="Pols"></param>
    Public Sub Crear_Polizas_Fiscales(ByVal Pols As List(Of Contabilizar_XML))
        Dim Pl As Contabilizar_XML
        If Pols.Count > 0 Then
            For Each Pl In Pols

                Dim Polis As String = Calcula_poliza(Pl.Fecha.Substring(3, 2), Pl.Fecha.Substring(6, 4), 1, 12)
                Dim Posi As Integer = InStr(1, Polis, "-", CompareMethod.Binary)
                Dim Cuantos As Integer = Len(Polis) - Len(Polis.Substring(0, Posi))
                Dim Consecutivo As Integer = Val(Polis.Substring(Posi, Cuantos))
                'crear poliza
                If Creapoliza(Polis, Pl.Fecha.Substring(6, 4), Pl.Fecha.Substring(3, 2), Pl.Fecha.Substring(0, 2),
                              Consecutivo, 12, Pl.Fecha, "Gtos / Factura " & Pl.UUID, "", Consecutivo,
                              Eventos.ObtenerValorDB("Xml_Sat", "Id_Registro_Xml", "   UUID = '" & Pl.UUID & "' ", True), Pl.UUID, Pl.Orden) Then
                    Pl.Poliza = Polis
                    'Generar detalle
                    If Buscafactura(Pl.UUID, "C") = True Then
                        'Se inserta la Factura
                        Eventos.Inserta_Comprobante_Fiscal(Polis, Pl.Fecha.Substring(6, 4), Pl.Fecha.Substring(3, 2),
                                 Pl.Emisor, Pl.Fecha, Pl.UUID, "Factura " & Trim(Pl.Emisor) & " C", Pl.Total)
                    Else
                        'Se Edita la Factura
                        Eventos.Edita_Factura(Pl.UUID, "C", Polis)
                    End If

                    If Pl.Metodo = "01 - Efectivo" Then
                        Eventos.Inserta_Comprobante_Fiscal_Efectivo(Polis, Pl.Fecha.Substring(6, 4), Pl.Fecha.Substring(3, 2),
                              Pl.Emisor, "001", Pl.Fecha,
                              "", "", "", Pl.Total, 1)
                    Else ' Transferencia
                        Eventos.Inserta_Comprobante_Fiscal_Transf(Polis, Pl.Fecha.Substring(6, 4), Pl.Fecha.Substring(3, 2),
                               Pl.Emisor, "001", Pl.Fecha,
                              "", "", "", Pl.Total, "02", 0, 1)
                    End If
                    Genera_Detalle(Pl, Polis)

                Else

                End If

            Next


        End If

    End Sub
    ''' <summary>
    ''' Esta funcion regresa la cuenta de acuerdo con el RFC solicitado y en caso de no existir en el catalogo de cuentas la crea de acuerdo a los parametros recibidos
    ''' </summary>
    ''' <param name="cuenta"></param>
    ''' <param name="RFCE"></param>
    ''' <param name="NombreE"></param>
    ''' <param name="Empresa"></param>
    ''' <param name="Clave"></param>
    ''' <param name="Tipo"></param>
    ''' <returns></returns>
    Public Function RegresaCuneta(ByVal cuenta As String, ByVal RFCE As String, ByVal NombreE As String, ByVal Empresa As Integer, ByVal Clave As String, Optional ByVal Tipo As Integer = 0)
        Dim Cta As String = ""
        Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & RFCE & "' and ID_Empresa = " & Empresa & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            'No existe la cuenta y la inserta
            If Tipo = 1 Then
                Cta = Val(Eventos.ObtenerValorDB("Catalogo_de_cuentas", "max (Nivel3 ) + 1 ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0 and ID_Empresa = " & Empresa & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
                               "0000", cuenta.Substring(0, 8) & Cta & "0000", RFCE & " " & NombreE,
                                Empresa, Clave, RFCE, NombreE)
                Cta = cuenta.Substring(0, 8) & Cta & "0000"
            Else
                Cta = Val(Eventos.ObtenerValorDB("Catalogo_de_cuentas", "max (Nivel4 ) + 1 ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & cuenta.ToString.Substring(8, 4) & " AND Nivel4 >= 0000 and ID_Empresa = " & Empresa & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), cuenta.ToString.Substring(8, 4),
                                  Cta, cuenta.Substring(0, 12) & Cta, RFCE & " " & NombreE,
                                  Empresa, Clave, RFCE, NombreE)
                Cta = cuenta.Substring(0, 12) & Cta
            End If
        End If
        Return Cta
    End Function
    ''' <summary>
    ''' esta Funcion Regresa la cuenta ligada al tipo de Egreso que se requiere contabilizar 
    ''' </summary>
    ''' <param name="cliente"></param>
    ''' <param name="tipo"> el tipo es la columna del egreso que se quiere contabilizar ejemplo: Egresos Gravados = CtaEgG </param>
    ''' <param name="serie"> es la clvae del egreso </param>
    ''' <param name="Tasa"> es el tipo de tasa a la que se grava </param>
    ''' <returns></returns>
    Public Function Regresa_Cuenta_Series(ByVal cliente As Integer, ByVal tipo As String, ByVal serie As String, ByVal Tasa As Decimal) As String

        Dim sql As String = "SELECT 	Id_ClaveR,	Clave,	 	CtaEgG,	CtaEgEx,	CtaEgC,	IVAAcre, CtaPEgG, CtaPEgEx,	CtaPEgC, IVAPAcre, ID_Empresa, Egresos,Deudor FROM dbo.ClavesRecibidas WHERE ID_Empresa = " & cliente & " and Egresos = '" & serie & "' and Tasa =" & Tasa & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Regresa_Cuenta_Series = Trim(ds.Tables(0).Rows(0)(tipo)).Substring(0, 12)
        Else

            Regresa_Cuenta_Series = ""
        End If
        Return Regresa_Cuenta_Series
    End Function
    ''' <summary>
    ''' Esta clase genera el detallede una poliza por item 
    ''' </summary>
    ''' <param name="id_poliza"> Este identificador se genera a partir de otra clase </param>
    ''' <param name="item"></param>
    ''' <param name="cargo"></param>
    ''' <param name="Abono"></param>
    ''' <param name="cuenta"></param>
    ''' <param name="cheque"></param>

    Public Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                       ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas"
        sql &= "(   "
        sql &= " ID_poliza,      "
        sql &= " ID_item,       "
        sql &= " Cargo,          "
        sql &= " Abono,         "
        sql &= " Fecha_captura,  "
        sql &= " Movto,"
        sql &= " Cuenta, "
        sql &= " No_cheque  "
        sql &= " ) "
        sql &= " VALUES "
        sql &= "( "
        sql &= " '" & id_poliza & "'	," '@id_poliza,     
        sql &= "" & item & "," '@id_item,       
        sql &= "" & cargo & "," '@cargo,         
        sql &= "" & Abono & "," '@abono,         
        sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
        sql &= " 'A'	," '@movto,         
        sql &= " " & cuenta & "	," '@cuenta,        
        sql &= " '" & cheque & "'" '@no_cheque      
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    ''' <summary>
    ''' Esta Funcion regresa las cuentas ligadas a los diferentes impuestos 
    ''' </summary>
    ''' <param name="cliente"></param>
    ''' <param name="tipo"> el tipo de impuesto que se requiere contabilizar </param>
    ''' <param name="serie"> el identificador de la clave del egreso </param>
    ''' <param name="Tasa"></param>
    ''' <returns></returns>
    Public Function Regresa_Cuenta_Impuestos(ByVal cliente As Integer, ByVal tipo As String, ByVal serie As String, ByVal Tasa As Decimal) As String
        Dim sql As String = "SELECT Id_ClaveR,	Clave,	IVAAcre, IVAPAcre	ID_Empresa FROM dbo.ClavesRecibidas WHERE ID_Empresa = " & cliente & " and Egresos = '" & serie & "' and Tasa =" & Tasa & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Regresa_Cuenta_Impuestos = Trim(ds.Tables(0).Rows(0)(tipo))
        Else
            Regresa_Cuenta_Impuestos = ""
        End If
        Return Regresa_Cuenta_Impuestos
    End Function
    ''' <summary>
    ''' Esta clase genera el detallado de polizas Automatizado para los gastos
    ''' </summary>
    ''' <param name="Pl"></param>
    ''' <param name="Po"></param>
    Private Sub Genera_Detalle(ByVal Pl As Contabilizar_XML, ByVal Po As String)
        Dim Cuenta1 As String = ""
        Dim Cuenta2 As String = ""
        Dim Item As Integer = 1
        If Pl.Serie = "Combustible" Then
            Pl.Importe_Grabado += Pl.Importe_Exento
            Pl.Importe_Exento = 0
        End If

        If Pl.Importe_Exento > 0 And Pl.Importe_Grabado > 0 And Pl.Iva > 0 Then ' tiene grabado y exento
            Cuenta1 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgG", Pl.Serie, Pl.Tasa), Pl.Emisor, Pl.Nombre, 1, "GG", 0)
            Cuenta2 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgEx", Pl.Serie, Pl.Tasa), Pl.Emisor, Pl.Nombre, 1, "GG", 0)
            Eventos.Crea_detalle_poliza(Po, Item, Pl.Importe_Grabado, 0, Cuenta1, "")
            Item += 1
            Crea_detalle_poliza(Po, Item, Pl.Importe_Exento, 0, Cuenta2, "")
            Item += 1
            Crea_detalle_poliza(Po, Item, Pl.Iva, 0, Eventos.Regresa_Cuenta_Impuestos(1, "IVAAcre", Pl.Serie, Pl.Tasa), "")
            Item += 1

        ElseIf Pl.Importe_Exento > 0 And Pl.Importe_Grabado <= 0 Then 'Tiene Exento o 0
            Cuenta2 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgEx", Pl.Serie, Pl.Tasa), Pl.Emisor, Pl.Nombre, 1, "GG", 0)
            Crea_detalle_poliza(Po, Item, Pl.Importe_Exento, 0, Cuenta2, "")
            Item += 1
        ElseIf Pl.Importe_Exento <= 0 And Pl.Importe_Grabado > 0 Then 'Tiene Grabado
            Cuenta1 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgG", Pl.Serie, Pl.Tasa), Pl.Emisor, Pl.Nombre, 1, "GG", 0)
            Eventos.Crea_detalle_poliza(Po, Item, Pl.Importe_Grabado, 0, Cuenta1, "")
            Item += 1
            Crea_detalle_poliza(Po, Item, Pl.Iva, 0, Eventos.Regresa_Cuenta_Impuestos(1, "IVAAcre", Pl.Serie, Pl.Tasa), "")
            Item += 1
        End If
        Cuenta1 = Eventos.RegresaCunetaDeudor(Eventos.Regresa_Cuenta_Series(1, "Deudor", Pl.Serie, Pl.Tasa), Pl.RFC_Deudor, Pl.Nombres_Deudor, 1, "GG", Pl.Matricula)
        Crea_detalle_poliza(Po, Item, 0, Pl.Total, Cuenta1, "")

    End Sub
    ''' <summary>
    ''' Esta clase genera una nueva cuenta en el catalogo de cuentas
    ''' </summary>
    ''' <param name="nivel1"></param>
    ''' <param name="nivel2"></param>
    ''' <param name="nivel3"></param>
    ''' <param name="nivel4"></param>
    ''' <param name="cuenta"></param>
    ''' <param name="descripcion"> Este campo es Obligatorio para reconocer la cuenta</param>
    ''' <param name="cliente"> Este campo es obligatorio y debe ser de tipo entero</param>
    ''' <param name="letra"></param>
    ''' <param name="RFC"> Este campo es indispensable para movimientos de Gasto </param>
    ''' <param name="Ali"></param>
    Public Sub Crear_cuenta(ByVal nivel1 As String, ByVal nivel2 As String, ByVal nivel3 As String,
                             ByVal nivel4 As String, ByVal cuenta As String, ByVal descripcion As String, ByVal cliente As Integer, ByVal letra As String, ByVal RFC As String, ByVal Ali As String)
        Dim ds As DataSet = Eventos.Obtener_DS("Select Naturaleza,Clasificacion,Balanza,Cta_ceros,Cta_Cargo_Cero,Cta_Abono_Cero from Catalogo_de_Cuentas where nivel1 ='" & cuenta.ToString.Substring(0, 4) & "' and ID_Empresa = " & cliente & "  ")

        If ds.Tables(0).Rows.Count > 0 Then
            Dim sql As String = ""
            sql = "INSERT INTO dbo.Catalogo_de_Cuentas "
            sql &= "("
            sql &= "Nivel1, "
            sql &= "Nivel2,"
            sql &= "Nivel3,"
            sql &= "Nivel4,"
            sql &= "Cuenta,"
            sql &= "Descripcion,"
            sql &= "Naturaleza,"
            sql &= "Clasificacion,"
            sql &= "Codigo_Agrupador,"
            If RFC <> "" Then
                sql &= "RFC,"
            Else
                sql &= "RFC,"
            End If
            sql &= "ID_Empresa,clave, "
            sql &= "Balanza,"
            sql &= "Cta_ceros,  "
            sql &= "Cta_Cargo_Cero,"
            sql &= "Cta_Abono_Cero ,Alias "

            sql &= "	)  "
            sql &= "VALUES  "
            sql &= "	(  "
            sql &= "	'" & nivel1 & "'," '@nivel1
            sql &= "	'" & nivel2 & "'," '@nivel2
            sql &= "	'" & nivel3 & "'," '@nivel3
            sql &= "	'" & nivel4 & "'," '@nivel4
            sql &= "	'" & cuenta & "'," '@cuenta
            sql &= "	'" & descripcion & "'," '@descripcion
            sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Naturaleza")) & "'," '@naturaleza
            sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Clasificacion")) & "'," '@clasificacion
            Dim su As String = ""
            If (nivel4 <> "0000" Or nivel3 <> "0000") And nivel2 <> "0000" Then
                su = nivel3.Substring(2, 2)
            End If
            sql &= "	'" & nivel1.ToString.Substring(0, 3) & su & "'," '@codigo_agrupador
            If RFC = "" Then
                sql &= "	NULL," '@RFC
            Else
                sql &= "	'" & RFC & "'," '@RFC
            End If
            sql &= "	" & cliente & " , '" & Trim(letra) & "'," '@ID_Empresa    

            sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Balanza"))) & "," '@Balanza
            sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_ceros"))) & "," '@Cta_ceros
            sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_Cargo_Cero"))) & "," '@Balanza
            sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_Abono_Cero"))) & " , '" & Ali.Trim() & "'" '@Cta_ceros
            sql &= "  )"
            ' Ingresar codigo para importar catalogos
            If Eventos.Comando_sql(sql) > 0 Then

            End If
        Else

        End If
    End Sub
    ''' <summary>
    ''' Esta funcion regeresa la cuenta del operador para los gastos de traslado
    ''' </summary>
    ''' <param name="cuenta"></param>
    ''' <param name="RFCE"> Este campo es obligatorio y es de tipo string con longitud de 13 caracteres</param>
    ''' <param name="NombreE"></param>
    ''' <param name="Empresa"></param>
    ''' <param name="Clave"></param>
    ''' <param name="Mat"> Este campo es obligatorio y debe ser en formato entero</param>
    ''' <returns></returns>
    Public Function RegresaCunetaDeudor(ByVal cuenta As String, ByVal RFCE As String, ByVal NombreE As String,
                                        ByVal Empresa As Integer, ByVal Clave As String, ByVal Mat As String)
        Dim Cta As String = ""
        Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND RFC = '" & RFCE & "' and ID_Empresa = " & Empresa & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
            Cta = Eventos.ObtenerValorDB("Catalogo_de_cuentas", "cuenta", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3 = " & Mat.PadLeft(4, "0") & " and nivel4 = 0001 and ID_Empresa = " & Empresa & "", True)
            If Cta = " " Then
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(8, 4),
                             "0001", cuenta.Substring(0, 8) & Cta.ToString.Substring(8, 4) & "0001", "Gastos de Traslado",
                              Empresa, Clave, RFCE, NombreE)
                Cta = cuenta.Substring(0, 8) & Cta.ToString.Substring(8, 4) & "0001"
            End If
        Else
            Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Mat.PadLeft(4, "0"),
                            "0000", cuenta.Substring(0, 8) & Mat.PadLeft(4, "0") & "0000", RFCE & " " & NombreE,
                             Empresa, Clave, RFCE, NombreE)

            Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Mat.PadLeft(4, "0"),
                            "0000", cuenta.Substring(0, 8) & Mat.PadLeft(4, "0") & "0001", "Gastos de Traslado",
                             Empresa, Clave, RFCE, NombreE)
            Cta = cuenta.Substring(0, 8) & Mat.PadLeft(4, "0") & "0001"
        End If
        Return Cta
    End Function
    ''' <summary>
    ''' Esta clase edita el registro de Facturas para ponerle el identificador de poliza
    ''' </summary>
    ''' <param name="Folio_Fiscal"> Este campo es obligatorio y corresponde al UUID </param>
    ''' <param name="Detaclle"></param>
    ''' <param name="Poliza"></param>
    Public Sub Edita_Factura(ByVal Folio_Fiscal As String, ByVal Detaclle As String, ByVal Poliza As String)
        Dim sql As String = " UPDATE dbo.Facturas SET ID_poliza = '" & Poliza & "' WHERE Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & Detaclle & "' "
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    ''' <summary>
    ''' Esta clase inserta el comprobante fiscal 
    ''' </summary>
    ''' <param name="Id_poliza"></param>
    ''' <param name="Anio"></param>
    ''' <param name="Mes"></param>
    ''' <param name="Rfc_Emisor"></param>
    ''' <param name="Fecha"></param>
    ''' <param name="Folio_Fiscal"></param>
    ''' <param name="Referencia"></param>
    ''' <param name="Importe"></param>
    Public Sub Inserta_Comprobante_Fiscal(ByVal Id_poliza As String, ByVal Anio As Integer, ByVal Mes As String,
                         ByVal Rfc_Emisor As String, ByVal Fecha As String,
                         ByVal Folio_Fiscal As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "INSERT INTO dbo.Facturas"
        sql &= " 	(                   "
        sql &= " 	ID_anio,                    "
        sql &= " 	ID_mes,                     "
        sql &= " 	ID_poliza,                  "
        sql &= " 	RFC_Emisor,                 "
        sql &= " 	Folio_Fiscal,               "
        sql &= " 	Referencia,                 "
        sql &= " 	Importe,                "
        sql &= " 	Fecha_Comprobante,          "
        sql &= " 	Detalle_Comp_Electronico,ID_Empresa"
        sql &= "    )                         "
        sql &= " VALUES "
        sql &= "(                             "
        sql &= " '" & Anio & "',	" '@id_anio,                   
        sql &= " '" & Mes & "'," '@id_mes,                    
        sql &= " '" & Id_poliza & "'," '@id_poliza,                 
        sql &= " '" & Rfc_Emisor & "'," '@rfc_emisor,                
        sql &= " '" & Folio_Fiscal & "'," '@folio_fiscal,              
        sql &= " '" & Referencia & "'," '@referencia,                
        sql &= " " & Importe & "	," '@importe,                   
        sql &= " " & Eventos.Sql_hoy(Fecha) & "," '@fecha_comprobante,         
        sql &= " 'C',1" '@detalle_comp_electronico   
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    ''' <summary>
    ''' Inserta el Comprobante fiscal para contabilidad electronica en efectivo
    ''' </summary>
    ''' <param name="id_poliza"></param>
    ''' <param name="anio"></param>
    ''' <param name="mes"></param>
    ''' <param name="Rfc_Emisor"></param>
    ''' <param name="tipo"></param>
    ''' <param name="fecha"></param>
    ''' <param name="No_cheque"></param>
    ''' <param name="no_banco"></param>
    ''' <param name="cuenta_origen"></param>
    ''' <param name="Importe"></param>
    ''' <param name="Empresa"></param>
    Public Sub Inserta_Comprobante_Fiscal_Efectivo(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                          ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                          ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Importe As Decimal, ByVal Empresa As Integer)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema(     anio,    mes,    tipo,       RFC_Ce, No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe, ID_poliza,    Tipo_CE,ID_Empresa	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'P' ," & Empresa & " " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    ''' <summary>
    ''' Inserta el Comprobante fiscal para contabilidad electronica en Transferencia
    ''' </summary>
    ''' <param name="id_poliza"></param>
    ''' <param name="anio"></param>
    ''' <param name="mes"></param>
    ''' <param name="Rfc_Emisor"></param>
    ''' <param name="tipo"></param>
    ''' <param name="fecha"></param>
    ''' <param name="No_cheque"></param>
    ''' <param name="no_banco"></param>
    ''' <param name="cuenta_origen"></param>
    ''' <param name="Importe"></param>
    ''' <param name="bancoD"></param>
    ''' <param name="cuentaD"></param>
    ''' <param name="Empresa"></param>
    Public Sub Inserta_Comprobante_Fiscal_Transf(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String,
                                                 ByVal Importe As Decimal, ByVal bancoD As String, ByVal cuentaD As String, ByVal Empresa As Integer)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema(anio,    mes,    tipo,       RFC_Ce, No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,ID_poliza,    Tipo_CE,Banco_Destino,Cuenta_Destino,ID_Empresa	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'T','" & Trim(bancoD) & "', " & cuentaD & "," & Empresa & " " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    ''' <summary>
    ''' Esta clase elimina las ligas de cada tabla para que el UUID pueda volver a utilizarce para la Commprobacion de gastos
    ''' </summary>
    ''' <param name="UUID"></param>
    ''' <param name="Empresa"></param>
    ''' <param name="Poliza"></param>
    Public Sub LiberarCarga_UUIDComp(ByVal UUID As String, ByVal Empresa As Integer, ByVal Poliza As String)

        Dim sql As String = ""
        sql = "UPDATE dbo.Facturas SET Id_Poliza = NULL WHERE Id_Poliza = '" & Trim(Poliza) & "'"
        If Eventos.Comando_sql(sql) >= 0 Then

        End If
        sql = "DELETE FROM Conta_E_Sistema  WHERE Id_Poliza = '" & Trim(Poliza) & "'"
        If Eventos.Comando_sql(sql) >= 0 Then

        End If

        sql = "DELETE From Detalle_Polizas WHERE Id_Poliza = '" & Trim(Poliza) & "' "
        If Eventos.Comando_sql(sql) >= 0 Then

        End If
        sql = "UPDATE dbo.Xml_Sat SET Id_Poliza = NULL WHERE Id_Poliza = '" & Trim(Poliza) & "'"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
        sql = "UPDATE dbo.XmlAuditados SET Id_PolizaS = NULL, Id_Poliza_Tusa =NULL WHERE Id_PolizaS = '" & Trim(Poliza) & "'"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
        sql = "DELETE From Polizas  WHERE Id_Poliza = '" & Trim(Poliza) & "'"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
        ' Liberar codigo de Tusa 
        Try
            sql = "SELECT XMLPolizas.Registro ,XMLPolizas.Id_orden ,XMLPolizas.Tipo , XMLPolizas.iva, XMLPolizas.UUDI, XMLPolizas.Total        From XMLPolizas  Where UUDI = '" & UUID & "'"
            Dim ds As DataSet = Eventos.Obtener_DSTusa(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    sql = " Update dbo.Talon_gastos SET Importe -= " & ds.Tables(0).Rows(i)("Total") - ds.Tables(0).Rows(0)("iva") & " , Iva -= " & ds.Tables(0).Rows(i)("iva") & " ,Autorizado -= " & ds.Tables(0).Rows(i)("Total") & "  where Id_Orden ='" & ds.Tables(0).Rows(i)("Id_Orden") & "' and ID_grupo = " & ds.Tables(i).Rows(0)("Tipo") & " "
                    If Eventos.Comando_sqlTusa(sql) > 0 Then
                        sql = "Delete From XMLPolizas where  UUDI = '" & UUID & "' and Id_Orden ='" & ds.Tables(0).Rows(i)("Id_Orden") & "' "
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Esta Clase permite quitar las retricciones para que comprobacion de gastos pueda usar el UUID en una Orden de Traslados
    ''' </summary>
    ''' <param name="UUID"></param>
    ''' <param name="Empresa"></param>
    Public Sub LiberarComprobacion(ByVal UUID As String, ByVal Empresa As Integer)
        Dim Sql As String = ""
        Try
            Sql = " Update dbo.XmlAuditados SET Errores =''  where UUID ='" & UUID & "' and Id_Empresa = " & Empresa & " "
            If Eventos.Comando_sql(Sql) > 0 Then
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Esta clase permite eliminar los registros ligados de polizas y detalle de polizas asi como  contabilidad electronica de un registro UUID
    ''' </summary>
    ''' <param name="Fi"> Campo obligatorio de Fecha de Inicio </param>
    ''' <param name="Fe"> Campo obligatorio de Fecha de Finalizacion </param>
    ''' <param name="Complemento"></param>
    ''' <param name="Factura"></param>
    ''' <param name="Cliente"></param>
    ''' <param name="Emitidas"></param>
    Public Sub LiberarCarga(ByVal Fi As String, ByVal Fe As String, ByVal Complemento As Boolean, ByVal Factura As Boolean, ByVal Cliente As Integer, ByVal Emitidas As Integer)

        Dim cade As String = ""
        If Factura = True And Complemento = True Then
            cade = "INNER JOIN Xml_Complemento ON Xml_Complemento.Id_Poliza = Polizas.Id_Poliza INNER JOIN Xml_Sat ON Xml_Sat.Id_Poliza = Polizas.Id_Poliza"
        ElseIf Factura = False And Complemento = True Then
            cade = "INNER JOIN Xml_Complemento ON Xml_Complemento.Id_Poliza = Polizas.Id_Poliza"
        ElseIf Factura = True And Complemento = False Then
            cade = "INNER JOIN Xml_Sat ON Xml_Sat.Id_Poliza = Polizas.Id_Poliza"
        End If


        Dim sql As String = " Select Polizas.Id_Poliza From Polizas " & cade & " 
        where Fecha >= " & Fi & " AND Fecha  <= " & Fe & "  
                        AND Polizas.Id_Empresa =" & Cliente & " and Emitidas =" & Emitidas & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ' Eliminar Facturas
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                sql = "UPDATE dbo.Facturas SET Id_Poliza = NULL WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                If Eventos.Comando_sql(sql) >= 0 Then
                    Eventos.Insertar_usuariol("LibFacturas", sql)
                    sql = "DELETE FROM Conta_E_Sistema  WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                    If Eventos.Comando_sql(sql) >= 0 Then
                        'Se elimina la contabilidad electronica
                        Eventos.Insertar_usuariol("LibContaElec", sql)
                        sql = " DELETE From Detalle_Polizas  WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "' "
                        If Eventos.Comando_sql(sql) >= 0 Then
                            'Se elimina el Detalle de Gastos
                            Eventos.Insertar_usuariol("EliminaDetalle", sql)

                            If Complemento = True And Factura = False Then 'Complementos
                                sql = "UPDATE dbo.Xml_Complemento SET Id_Poliza = NULL WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Se libera el Complemento
                                    Eventos.Insertar_usuariol("LiberaCompto", sql)
                                    sql = "DELETE From Polizas  WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        'Eliminar las polizas
                                        Eventos.Insertar_usuariol("EliminaPolizas", sql)
                                    End If
                                End If

                            ElseIf Complemento = False And Factura = True Then ' Facturas
                                sql = "UPDATE dbo.Xml_Sat SET Id_Poliza = NULL WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Se libera el XML
                                    Eventos.Insertar_usuariol("LiberaXML", sql)
                                    sql = "DELETE From Polizas  WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        'Eliminar las polizas
                                        Eventos.Insertar_usuariol("EliminaPolizas", sql)
                                    End If
                                End If
                            ElseIf Complemento = True And Factura = True Then ' AMBAS 
                                sql = "UPDATE dbo.Xml_Complemento SET Id_Poliza = NULL WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Se libera el Complemento
                                    Eventos.Insertar_usuariol("LiberaCompto", sql)
                                End If
                                sql = "UPDATE dbo.Xml_Sat SET Id_Poliza = NULL WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Se libera el XML
                                    Eventos.Insertar_usuariol("LiberaXML", sql)
                                End If
                                sql = "DELETE From Polizas  WHERE Id_Poliza = '" & Trim(ds.Tables(0).Rows(i)("Id_Poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Eliminar las polizas
                                    Eventos.Insertar_usuariol("EliminaPolizas", sql)
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End If

    End Sub
    ''' <summary>
    ''' Esta funcion permite realizar una operacion Insert, Update, Delete dentro de la base de datos Tusa
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    Public Function Comando_sqlTusa(ByVal sql) As Integer
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim source As String
        source = "Data Source= 2.tcp.ngrok.io,13130 ;Persist Security Info=True;User ID=tusasa;password=51s73m452019;Initial Catalog=Tusa"
        Dim Conn As New SqlConnection(source)
        Try
            Conn.Open()
            Dim cmdsql As New SqlClient.SqlCommand
            cmdsql.CommandText = sql
            cmdsql.Connection = Conn
            Comando_sqlTusa = cmdsql.ExecuteNonQuery()
            Conn.Close()
        Catch ex As Exception
            Comando_sqlTusa = 0
            RadMessageBox.Show("No se pudieron guardar los datos: " & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
        Return Comando_sqlTusa
    End Function
    ''' <summary>
    ''' Esta clase permite evaluar un XML 
    ''' </summary>
    Public Class Evaluador_XML
        Public Property UUID As String
        Public Property Orden As String
        Public Property Importe_Grabado As Decimal
        Public Property Importe_Exento As Decimal
        Public Property Total As Decimal
        Public Property Iva As Decimal
        Public Property Tasa As Decimal
        Public Property Estatus As String
        Public Property Fecha As String
        Public Property Emisor As String
        Public Property Nombre As String
        Public Property Metodo As String
    End Class
    ''' <summary>
    ''' Esta clase permite liberar las parcialidades de un UUID que no tenga comprobante fiscal digital
    ''' </summary>
    ''' <param name="UUID"></param>
    ''' <param name="Cliente"></param>
    ''' <param name="Poliza"></param>
    Public Sub LiberarCargaPagos(ByVal UUID As String, ByVal Cliente As Integer, ByVal Poliza As String)


        ' Eliminar Facturas

        Dim Sql = "UPDATE dbo.Facturas SET ID_poliza  = NULL WHERE ID_poliza  = '" & Poliza.Trim() & "'"
        If Eventos.Comando_sql(Sql) >= 0 Then
            Eventos.Insertar_usuariol("LibFacturas", Sql)
            Sql = "DELETE FROM Conta_E_Sistema  WHERE ID_poliza  = '" & Poliza.Trim() & "'"
            If Eventos.Comando_sql(Sql) >= 0 Then
                'Se elimina la contabilidad electronica
                Eventos.Insertar_usuariol("LibContaElec", Sql)
                Sql = " DELETE From Detalle_Polizas  WHERE ID_poliza  = '" & Poliza.Trim() & "' "
                If Eventos.Comando_sql(Sql) >= 0 Then
                    'Se elimina el Detalle de Gastos
                    Eventos.Insertar_usuariol("EliminaDetalle", Sql)

                    Sql = "DELETE From Polizas   WHERE ID_poliza = '" & Poliza.Trim() & "'"
                    If Eventos.Comando_sql(Sql) > 0 Then
                        'Eliminar las polizas
                        Eventos.Insertar_usuariol("EliminaPolizas", Sql)

                        'Se libera el Complemento
                        Eventos.Insertar_usuariol("LiberaCompto", Sql)
                        Sql = "DELETE FROM dbo.Parcialidades  WHERE ID_poliza  = '" & Poliza.Trim() & "'"
                        If Eventos.Comando_sql(Sql) > 0 Then
                            'Eliminar las polizas
                            Eventos.Insertar_usuariol("EliminaPolizas", Sql)
                            Sql = "UPDATE Xml_Sat SET Tiene_Comple = 0  WHERE UUID = '" & UUID.Trim() & "' and Id_Empresa  = " & Cliente & ""
                            If Eventos.Comando_sql(Sql) >= 0 Then

                            End If
                        End If
                    End If
                End If

            End If
        End If



    End Sub
    ''' <summary>
    ''' Esta funcion permite cargar un archivo de tipo excel a un dataset
    ''' </summary>
    ''' <param name="archivo"></param>
    ''' <param name="NomHoja"></param>
    ''' <returns></returns>
    Public Function CargarExcelStandar(ByVal archivo As String, Optional ByVal NomHoja As String = "") As DataSet

        'cargarlo en la datagrid
        Try
            Dim strconn As String, dt As New DataSet
            strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & archivo & ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'"
            Dim mconn As New OleDbConnection(strconn)
            Dim ad As New OleDbDataAdapter("Select * from [" & IIf(NomHoja = "", "hoja1", NomHoja) & "$]", mconn)
            mconn.Open()
            ad.Fill(dt)
            mconn.Close()
            Return dt
        Catch ex As OleDbException
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Esta Funcion regresa un decimal con el cruce para calcular el IVA Acreditable
    ''' </summary>
    ''' <param name="Cliente"></param>
    ''' <param name="Anio"></param>
    ''' <param name="Mes"></param>
    ''' <returns></returns>
    Public Function IvaAcreditableCruces(ByVal Cliente As Integer, ByVal Anio As Integer, ByVal Mes As Integer)
        Dim Importe As Decimal = 0
        Dim sql As String = "SELECT   Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Anual, Anio, Id_Empresa,suma
                            FROM dbo.ImpuestosPMPF
                            WHERE Pm=1 AND Anio = " & Anio & "  AND Id_Empresa = " & Cliente & " 
                            AND  Suma  ='32.1'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Importe = ds.Tables(0).Rows(0)(Mes)
        Else
            Importe = 0
        End If
        Return Importe
    End Function
    ''' <summary>
    ''' Esta funcion regresa un DataSet a partir de un archivo de tipo excel.
    ''' </summary>
    ''' <param name="NomHoja"></param>
    ''' <returns></returns>
    Public Function CargarExcelDS2(Optional ByVal NomHoja As String = "") As DataSet
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            '.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Return Nothing
            End If
        End With
        'cargarlo en la datagrid
        Try
            Dim strconn As String, dt As New DataSet
            strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & archivo & ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'"
            Dim mconn As New OleDbConnection(strconn)
            Dim ad As New OleDbDataAdapter("Select * from [" & IIf(NomHoja = "", "hoja1", NomHoja) & "$]", mconn)
            mconn.Open()
            ad.Fill(dt)
            mconn.Close()
            Return dt
        Catch ex As OleDbException
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function


    Public Sub LiberarCarga_UUID(ByVal UUID As String, ByVal Complemento As Boolean, ByVal Factura As Boolean, ByVal Cliente As Integer, ByVal Emitidas As Integer)

        Dim cade As String = ""
        If Factura = True And Complemento = True Then
            cade = "INNER JOIN Xml_Complemento ON Xml_Complemento.ID_poliza = Polizas.ID_poliza INNER JOIN Xml_Sat ON Xml_Sat.ID_poliza = Polizas.ID_poliza"
        ElseIf Factura = False And Complemento = True Then
            cade = "INNER JOIN Xml_Complemento ON Xml_Complemento.ID_poliza = Polizas_Sistema.ID_poliza"
        ElseIf Factura = True And Complemento = False Then
            cade = "INNER JOIN Xml_Sat ON Xml_Sat.ID_poliza = Polizas.ID_poliza"
        End If


        Dim sql As String = " Select Polizas.ID_poliza From Polizas " & cade & " 
        where Polizas.Id_Empresa =" & Cliente & " and Emitidas =" & Emitidas & " AND UUID ='" & UUID & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ' Eliminar Facturas
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                sql = "UPDATE dbo.Facturas SET ID_poliza = NULL WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                If Eventos.Comando_sql(sql) >= 0 Then
                    Eventos.Insertar_usuariol("LibFacturas", sql)
                    sql = "DELETE FROM Conta_E_Sistema  WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                    If Eventos.Comando_sql(sql) >= 0 Then
                        'Se elimina la contabilidad electronica
                        Eventos.Insertar_usuariol("LibContaElec", sql)
                        sql = " DELETE From Detalle_Polizas WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "' "
                        If Eventos.Comando_sql(sql) >= 0 Then
                            'Se elimina el Detalle de Gastos
                            Eventos.Insertar_usuariol("EliminaDetalle", sql)

                            If Complemento = True And Factura = False Then 'Complementos
                                sql = "UPDATE dbo.Xml_Complemento SET ID_poliza = NULL WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Se libera el Complemento
                                    Eventos.Insertar_usuariol("LiberaCompto", sql)
                                    sql = "DELETE From Polizas_Sistema  WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        'Eliminar las polizas
                                        Eventos.Insertar_usuariol("EliminaPolizas", sql)
                                    End If
                                End If

                            ElseIf Complemento = False And Factura = True Then ' Facturas
                                sql = "UPDATE dbo.Xml_Sat SET ID_poliza = NULL WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Se libera el XML
                                    Eventos.Insertar_usuariol("LiberaXML", sql)
                                    sql = "DELETE From Polizas  WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        'Eliminar las polizas
                                        Eventos.Insertar_usuariol("EliminaPolizas", sql)
                                    End If
                                End If
                            ElseIf Complemento = True And Factura = True Then ' AMBAS 
                                sql = "UPDATE dbo.Xml_Complemento SET ID_poliza = NULL WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Se libera el Complemento
                                    Eventos.Insertar_usuariol("LiberaCompto", sql)
                                End If
                                sql = "UPDATE dbo.Xml_Sat SET ID_poliza = NULL WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Se libera el XML
                                    Eventos.Insertar_usuariol("LiberaXML", sql)
                                End If
                                sql = "DELETE From Polizas  WHERE ID_poliza = '" & Trim(ds.Tables(0).Rows(i)("ID_poliza")) & "'"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    'Eliminar las polizas
                                    Eventos.Insertar_usuariol("EliminaPolizas", sql)
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End If

    End Sub
End Module
