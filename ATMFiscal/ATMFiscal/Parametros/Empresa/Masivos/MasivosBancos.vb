Public Class MasivosBancos
    Public Event Registro(ByVal clave As String)
    Private Sub Cargar()
        Me.Masivos1.SqlSelect = "Select        '02' as Clave,   'BANAMEX' as Descripcion Union select         '06' as Clave,   'BANCOMEXT' as Descripcion Union select         '09' as Clave,   'BANOBRAS' as Descripcion Union select 
        '12' as Clave,  'BBVA BANCOMER' as Descripcion Union select         '14' as Clave,  'SANTANDER' as Descripcion Union select         '19' as Clave,  'BANJERCITO' as Descripcion Union select 
        '21' as Clave,  'HSBC' as Descripcion Union select         '30' as Clave,  'BAJIO' as Descripcion Union select         '32' as Clave,  'IXE' as Descripcion Union select 
        '36' as Clave,  'INBURSA' as Descripcion Union select         '37' as Clave,  'INTERACCIONES' as Descripcion Union select         '42' as Clave,  'MIFEL' as Descripcion Union select 
        '44' as Clave,  'SCOTIABANK' as Descripcion Union select         '58' as Clave,  'BANREGIO' as Descripcion Union select         '59' as Clave,  'INVEX' as Descripcion Union select 
        '60' as Clave,  'BANSI' as Descripcion Union select         '62' as Clave,  'AFIRME' as Descripcion Union select         '72' as Clave,  'BANORTE' as Descripcion Union select 
        '102' as Clave, 'THE ROYAL BANK' as Descripcion Union select         '103' as Clave, 'AMERICAN EXPRESS' as Descripcion Union select         '106' as Clave, 'BAMSA' as Descripcion Union select 
        '108' as Clave, 'TOKYO' as Descripcion Union select         '110' as Clave, 'JP MORGAN' as Descripcion Union select         '112' as Clave, 'BMONEX' as Descripcion Union select 
        '113' as Clave, 'VE POR MAS' as Descripcion Union select         '116' as Clave, 'ING' as Descripcion Union select         '124' as Clave, 'DEUTSCHE' as Descripcion Union select 
        '126' as Clave, 'CREDIT SUISSE' as Descripcion Union select         '127' as Clave, 'AZTECA' as Descripcion Union select         '128' as Clave, 'AUTOFIN' as Descripcion Union select 
        '129' as Clave, 'BARCLAYS' as Descripcion Union select         '130' as Clave, 'COMPARTAMOS' as Descripcion Union select         '131' as Clave, 'BANCO FAMSA' as Descripcion Union select         '132' as Clave, 'BMULTIVA' as Descripcion Union select 
        '133' as Clave, 'ACTINVER' as Descripcion Union select         '134' as Clave, 'WAL-MART' as Descripcion Union select         '135' as Clave, 'NAFIN' as Descripcion Union select         '136' as Clave, 'INTERBANCO' as Descripcion Union select 
        '137' as Clave, 'BANCOPPEL' as Descripcion Union select         '138' as Clave, 'ABC CAPITAL' as Descripcion Union select         '139' as Clave, 'UBS BANK' as Descripcion Union select         '140' as Clave, 'CONSUBANCO' as Descripcion Union select 
        '141' as Clave, 'VOLKSWAGEN' as Descripcion Union select         '143' as Clave, 'CIBANCO' as Descripcion Union select         '145' as Clave, 'BBASE' as Descripcion Union select         '166' as Clave, 'BANSEFI' as Descripcion Union select 
        '168' as Clave, 'HIPOTECARIA FEDERAL' as Descripcion Union select         '600' as Clave, 'MONEXCB' as Descripcion Union select         '601' as Clave, 'GBM' as Descripcion Union select         '602' as Clave, 'MASARI' as Descripcion Union select 
        '605' as Clave, 'VALUE' as Descripcion Union select         '606' as Clave, 'ESTRUCTURADORES' as Descripcion Union select         '607' as Clave, 'TIBER' as Descripcion Union select         '608' as Clave, 'VECTOR' as Descripcion Union select 
        '610' as Clave, 'B&B' as Descripcion Union select         '614' as Clave, 'ACCIVAL' as Descripcion Union select         '615' as Clave, 'MERRILL LYNCH' as Descripcion Union    select     '616' as Clave, 'FINAMEX' as Descripcion Union select 
        '617' as Clave, 'VALMEX' as Descripcion Union select         '618' as Clave, 'UNICA' as Descripcion Union select         '619' as Clave, 'MAPFRE' as Descripcion Union select         '620' as Clave, 'PROFUTURO' as Descripcion Union select 
        '621' as Clave, 'CB ACTINVER ' as Descripcion Union select         '622' as Clave, 'OACTIN' as Descripcion Union select         '623' as Clave, 'SKANDIA' as Descripcion Union select         '626' as Clave, 'CBDEUTSCHE' as Descripcion Union select 
        '627' as Clave, 'ZURICH' as Descripcion Union select         '628' as Clave, 'ZURICHVI' as Descripcion Union select         '629' as Clave, 'SU CASITA' as Descripcion Union select         '630' as Clave, 'CB INTERCAM' as Descripcion Union select 
        '631' as Clave, 'CI BOLSA' as Descripcion Union select         '632' as Clave, 'BULLTICK CB' as Descripcion Union select         '633' as Clave, 'STERLING' as Descripcion Union select         '634' as Clave, 'FINCOMUN' as Descripcion Union select 
        '636' as Clave, 'HDI SEGUROS' as Descripcion Union select         '637' as Clave, 'ORDER' as Descripcion Union select         '638' as Clave, 'AKALA' as Descripcion Union select         '640' as Clave, 'CB JPMORGAN' as Descripcion Union select 
        '642' as Clave, 'REFORMA' as Descripcion Union select         '646' as Clave, 'STP' as Descripcion Union select         '647' as Clave, 'TELECOMM' as Descripcion Union select         '648' as Clave, 'EVERCORE' as Descripcion Union select 
        '649' as Clave, 'SKANDIA' as Descripcion Union select         '651' as Clave, 'SEGMTY' as Descripcion Union select         '652' as Clave, 'ASEA'  as Descripcion Union select         '653' as Clave, 'KUSPIT' as Descripcion Union select 
        '655' as Clave, 'SOFIEXPRESS' as Descripcion Union select         '656' as Clave, 'UNAGRA' as Descripcion Union select         '659' as Clave, 'OPCIONES EMPRESARIALES DEL NOROESTE' as Descripcion Union select         '901' as Clave, 'CLS' as Descripcion Union select 
        '902' as Clave, 'INDEVAL' as Descripcion Union select         '670' as Clave, 'LIBERTAD' as Descripcion  order by Clave "

        Me.Masivos1.Cargar()
        For i As Integer = 0 To Me.Masivos1.Tabla.Rows.Count - 1
            VeriF(Me.Masivos1.Tabla.Item(1, i).Value, i)
        Next
    End Sub

    Private Sub Masivos1_Cmd_Editar(clave As String) Handles Masivos1.Cmd_Editar

        For i As Integer = 0 To Me.Masivos1.Tabla.Rows.Count - 1
            If Me.Masivos1.Tabla.Item(0, i).Value = True Then
                If VerificaExistencia(Me.Masivos1.Tabla.Item(1, i).Value) = True Then

                    Dim sql As String = "INSERT INTO dbo.Bancos"
                    sql &= " 	("
                    sql &= " 	 clave,Nombre"
                    sql &= " 	)"
                    sql &= " VALUES ("
                    sql &= " 	'" & Trim(Me.Masivos1.Tabla.Item(1, i).Value) & "' ,'" & Trim(Me.Masivos1.Tabla.Item(2, i).Value) & "'" '@nombre
                    sql &= " 	)"
                    If Eventos.Comando_sql(sql) = 1 Then
                        ' MessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Eventos.Insertar_usuariol("Bancos", sql)
                    Else
                        MessageBox.Show("Error al insertar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If


                End If
            End If
        Next
        Control_Bancos.Tabla_detalleBancos.CmdRefrescar.PerformClick()
        Me.Close()
    End Sub

    Private Sub Masivos1_Cerrar() Handles Masivos1.Cerrar
        Me.Close()
    End Sub

    Private Function VerificaExistencia(ByVal clave As String)
        Dim Hacer As Boolean
        Dim sql As String = "select * from Bancos where   clave = '" & Trim(clave) & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Hacer = False
        Else
            Hacer = True
        End If
        Return Hacer
    End Function
    Private Sub VeriF(ByVal clave As String, ByVal i As Integer)
        Dim sql As String = "select * from Bancos where  clave = '" & Trim(clave) & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Masivos1.Tabla.Item(0, i).Value = True
        Else
            Me.Masivos1.Tabla.Item(0, i).Value = False
        End If
    End Sub

    Private Sub Masivos1_Load(sender As Object, e As EventArgs) Handles Masivos1.Load
        Cargar()
    End Sub



    Private Sub Masivos1_Registro(clave As String) Handles Masivos1.Registro
        If Masivos1.Tabla.RowCount > 0 Then
            Me.LblFiltro.Text = Me.Masivos1.Tabla.Columns(Me.Masivos1.Tabla.CurrentCell.ColumnIndex).HeaderText
        End If
    End Sub

    Private Sub TxtFiltro_TextChanged(sender As Object, e As EventArgs) Handles TxtFiltro.TextChanged
        If Me.LblFiltro.Text <> "" Then
            Dim posicion As Integer = 0
            Dim sql As String = " SELECT* FROM (Select        '02' as Clave,   'BANAMEX' as Descripcion Union select         '06' as Clave,   'BANCOMEXT' as Descripcion Union select         '09' as Clave,   'BANOBRAS' as Descripcion Union select 
        '12' as Clave,  'BBVA BANCOMER' as Descripcion Union select         '14' as Clave,  'SANTANDER' as Descripcion Union select         '19' as Clave,  'BANJERCITO' as Descripcion Union select 
        '21' as Clave,  'HSBC' as Descripcion Union select         '30' as Clave,  'BAJIO' as Descripcion Union select         '32' as Clave,  'IXE' as Descripcion Union select 
        '36' as Clave,  'INBURSA' as Descripcion Union select         '37' as Clave,  'INTERACCIONES' as Descripcion Union select         '42' as Clave,  'MIFEL' as Descripcion Union select 
        '44' as Clave,  'SCOTIABANK' as Descripcion Union select         '58' as Clave,  'BANREGIO' as Descripcion Union select         '59' as Clave,  'INVEX' as Descripcion Union select 
        '60' as Clave,  'BANSI' as Descripcion Union select         '62' as Clave,  'AFIRME' as Descripcion Union select         '72' as Clave,  'BANORTE' as Descripcion Union select 
        '102' as Clave, 'THE ROYAL BANK' as Descripcion Union select         '103' as Clave, 'AMERICAN EXPRESS' as Descripcion Union select         '106' as Clave, 'BAMSA' as Descripcion Union select 
        '108' as Clave, 'TOKYO' as Descripcion Union select         '110' as Clave, 'JP MORGAN' as Descripcion Union select         '112' as Clave, 'BMONEX' as Descripcion Union select 
        '113' as Clave, 'VE POR MAS' as Descripcion Union select         '116' as Clave, 'ING' as Descripcion Union select         '124' as Clave, 'DEUTSCHE' as Descripcion Union select 
        '126' as Clave, 'CREDIT SUISSE' as Descripcion Union select         '127' as Clave, 'AZTECA' as Descripcion Union select         '128' as Clave, 'AUTOFIN' as Descripcion Union select 
        '129' as Clave, 'BARCLAYS' as Descripcion Union select         '130' as Clave, 'COMPARTAMOS' as Descripcion Union select         '131' as Clave, 'BANCO FAMSA' as Descripcion Union select         '132' as Clave, 'BMULTIVA' as Descripcion Union select 
        '133' as Clave, 'ACTINVER' as Descripcion Union select         '134' as Clave, 'WAL-MART' as Descripcion Union select         '135' as Clave, 'NAFIN' as Descripcion Union select         '136' as Clave, 'INTERBANCO' as Descripcion Union select 
        '137' as Clave, 'BANCOPPEL' as Descripcion Union select         '138' as Clave, 'ABC CAPITAL' as Descripcion Union select         '139' as Clave, 'UBS BANK' as Descripcion Union select         '140' as Clave, 'CONSUBANCO' as Descripcion Union select 
        '141' as Clave, 'VOLKSWAGEN' as Descripcion Union select         '143' as Clave, 'CIBANCO' as Descripcion Union select         '145' as Clave, 'BBASE' as Descripcion Union select         '166' as Clave, 'BANSEFI' as Descripcion Union select 
        '168' as Clave, 'HIPOTECARIA FEDERAL' as Descripcion Union select         '600' as Clave, 'MONEXCB' as Descripcion Union select         '601' as Clave, 'GBM' as Descripcion Union select         '602' as Clave, 'MASARI' as Descripcion Union select 
        '605' as Clave, 'VALUE' as Descripcion Union select         '606' as Clave, 'ESTRUCTURADORES' as Descripcion Union select         '607' as Clave, 'TIBER' as Descripcion Union select         '608' as Clave, 'VECTOR' as Descripcion Union select 
        '610' as Clave, 'B&B' as Descripcion Union select         '614' as Clave, 'ACCIVAL' as Descripcion Union select         '615' as Clave, 'MERRILL LYNCH' as Descripcion Union    select     '616' as Clave, 'FINAMEX' as Descripcion Union select 
        '617' as Clave, 'VALMEX' as Descripcion Union select         '618' as Clave, 'UNICA' as Descripcion Union select         '619' as Clave, 'MAPFRE' as Descripcion Union select         '620' as Clave, 'PROFUTURO' as Descripcion Union select 
        '621' as Clave, 'CB ACTINVER ' as Descripcion Union select         '622' as Clave, 'OACTIN' as Descripcion Union select         '623' as Clave, 'SKANDIA' as Descripcion Union select         '626' as Clave, 'CBDEUTSCHE' as Descripcion Union select 
        '627' as Clave, 'ZURICH' as Descripcion Union select         '628' as Clave, 'ZURICHVI' as Descripcion Union select         '629' as Clave, 'SU CASITA' as Descripcion Union select         '630' as Clave, 'CB INTERCAM' as Descripcion Union select 
        '631' as Clave, 'CI BOLSA' as Descripcion Union select         '632' as Clave, 'BULLTICK CB' as Descripcion Union select         '633' as Clave, 'STERLING' as Descripcion Union select         '634' as Clave, 'FINCOMUN' as Descripcion Union select 
        '636' as Clave, 'HDI SEGUROS' as Descripcion Union select         '637' as Clave, 'ORDER' as Descripcion Union select         '638' as Clave, 'AKALA' as Descripcion Union select         '640' as Clave, 'CB JPMORGAN' as Descripcion Union select 
        '642' as Clave, 'REFORMA' as Descripcion Union select         '646' as Clave, 'STP' as Descripcion Union select         '647' as Clave, 'TELECOMM' as Descripcion Union select         '648' as Clave, 'EVERCORE' as Descripcion Union select 
        '649' as Clave, 'SKANDIA' as Descripcion Union select         '651' as Clave, 'SEGMTY' as Descripcion Union select         '652' as Clave, 'ASEA'  as Descripcion Union select         '653' as Clave, 'KUSPIT' as Descripcion Union select 
        '655' as Clave, 'SOFIEXPRESS' as Descripcion Union select         '656' as Clave, 'UNAGRA' as Descripcion Union select         '659' as Clave, 'OPCIONES EMPRESARIALES DEL NOROESTE' as Descripcion Union select         '901' as Clave, 'CLS' as Descripcion Union select 
        '902' as Clave, 'INDEVAL' as Descripcion Union select         '670' as Clave, 'LIBERTAD' as Descripcion  ) AS tabla WHERE  " & Me.LblFiltro.Text & " like '%" & Me.TxtFiltro.Text & "%' order by Clave "
            Dim ds As DataSet = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.Masivos1.SqlSelect = sql
                Me.Masivos1.Cargar()
                For i As Integer = 0 To Me.Masivos1.Tabla.Rows.Count - 1
                    VeriF(Me.Masivos1.Tabla.Item(1, i).Value, i)
                Next
            End If
        End If
    End Sub
End Class
