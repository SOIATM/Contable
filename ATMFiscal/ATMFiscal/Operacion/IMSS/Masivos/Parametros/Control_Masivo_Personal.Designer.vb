<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Control_Masivo_Personal
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Control_Masivo_Personal))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.LblFiltro = New System.Windows.Forms.Label()
        Me.lblRegistros = New System.Windows.Forms.Label()
        Me.TxtFiltro = New Telerik.WinControls.UI.RadTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmdEliminarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdMovimientos = New Telerik.WinControls.UI.RadButton()
        Me.CmdAsignaDelegacion = New Telerik.WinControls.UI.RadButton()
        Me.CmdMasivo = New Telerik.WinControls.UI.RadButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Val = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombres = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Appaterno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ap_materno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RegistroPatronal = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DigVerificadorRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoIMSS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DigVerifIMSS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SalarioBase = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TipoTrabajador = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TipoSalario = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.SemJornadaReducida = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.UnidadMedicinaFamiliar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Guia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CURP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fechaalta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha_Baja = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LstTexto = New ATMFiscal.Listas()
        Me.LstSubDelegacion = New ATMFiscal.Listas()
        Me.LstDelegacion = New ATMFiscal.Listas()
        Me.lstCliente = New ATMFiscal.Listas()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdMovimientos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdAsignaDelegacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdMasivo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.CadetBlue
        Me.RadPanel1.Controls.Add(Me.LblFiltro)
        Me.RadPanel1.Controls.Add(Me.lblRegistros)
        Me.RadPanel1.Controls.Add(Me.TxtFiltro)
        Me.RadPanel1.Controls.Add(Me.LstTexto)
        Me.RadPanel1.Controls.Add(Me.Label5)
        Me.RadPanel1.Controls.Add(Me.Label4)
        Me.RadPanel1.Controls.Add(Me.LstSubDelegacion)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.LstDelegacion)
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.CmdEliminarF)
        Me.RadPanel1.Controls.Add(Me.CmdMovimientos)
        Me.RadPanel1.Controls.Add(Me.CmdAsignaDelegacion)
        Me.RadPanel1.Controls.Add(Me.CmdMasivo)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1268, 233)
        Me.RadPanel1.TabIndex = 1
        '
        'LblFiltro
        '
        Me.LblFiltro.AutoSize = True
        Me.LblFiltro.BackColor = System.Drawing.Color.CadetBlue
        Me.LblFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFiltro.Location = New System.Drawing.Point(84, 72)
        Me.LblFiltro.Name = "LblFiltro"
        Me.LblFiltro.Size = New System.Drawing.Size(0, 18)
        Me.LblFiltro.TabIndex = 700
        '
        'lblRegistros
        '
        Me.lblRegistros.AutoSize = True
        Me.lblRegistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistros.Location = New System.Drawing.Point(12, 210)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.Size = New System.Drawing.Size(112, 18)
        Me.lblRegistros.TabIndex = 639
        Me.lblRegistros.Text = "Total de registros:"
        '
        'TxtFiltro
        '
        Me.TxtFiltro.AutoSize = False
        Me.TxtFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFiltro.Location = New System.Drawing.Point(15, 103)
        Me.TxtFiltro.Name = "TxtFiltro"
        Me.TxtFiltro.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtFiltro.Size = New System.Drawing.Size(424, 36)
        Me.TxtFiltro.TabIndex = 638
        Me.TxtFiltro.ThemeName = "Material"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.CadetBlue
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 18)
        Me.Label5.TabIndex = 636
        Me.Label5.Text = "Filtrar:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 18)
        Me.Label4.TabIndex = 635
        Me.Label4.Text = "Valor a Asignar:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(458, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 18)
        Me.Label2.TabIndex = 633
        Me.Label2.Text = "Sub Delegación:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(457, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 18)
        Me.Label1.TabIndex = 631
        Me.Label1.Text = "Delegación:"
        '
        'CmdEliminarF
        '
        Me.CmdEliminarF.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarF.Location = New System.Drawing.Point(227, 11)
        Me.CmdEliminarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminarF.Name = "CmdEliminarF"
        Me.CmdEliminarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminarF.TabIndex = 629
        Me.CmdEliminarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminarF.ThemeName = "Aqua"
        '
        'CmdMovimientos
        '
        Me.CmdMovimientos.Image = Global.ATMFiscal.My.Resources.Resources.EXPORTAR_TXT
        Me.CmdMovimientos.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdMovimientos.Location = New System.Drawing.Point(335, 11)
        Me.CmdMovimientos.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdMovimientos.Name = "CmdMovimientos"
        Me.CmdMovimientos.Size = New System.Drawing.Size(50, 54)
        Me.CmdMovimientos.TabIndex = 627
        Me.CmdMovimientos.TabStop = False
        Me.CmdMovimientos.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdMovimientos.ThemeName = "Aqua"
        '
        'CmdAsignaDelegacion
        '
        Me.CmdAsignaDelegacion.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdAsignaDelegacion.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdAsignaDelegacion.Location = New System.Drawing.Point(389, 11)
        Me.CmdAsignaDelegacion.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdAsignaDelegacion.Name = "CmdAsignaDelegacion"
        Me.CmdAsignaDelegacion.Size = New System.Drawing.Size(50, 54)
        Me.CmdAsignaDelegacion.TabIndex = 630
        Me.CmdAsignaDelegacion.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdAsignaDelegacion.ThemeName = "Aqua"
        '
        'CmdMasivo
        '
        Me.CmdMasivo.Image = Global.ATMFiscal.My.Resources.Resources.Importar_Datos
        Me.CmdMasivo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdMasivo.Location = New System.Drawing.Point(281, 11)
        Me.CmdMasivo.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdMasivo.Name = "CmdMasivo"
        Me.CmdMasivo.Size = New System.Drawing.Size(50, 54)
        Me.CmdMasivo.TabIndex = 628
        Me.CmdMasivo.TabStop = False
        Me.CmdMasivo.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdMasivo.ThemeName = "Aqua"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(457, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 625
        Me.Label3.Text = "Empresa:"
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(11, 11)
        Me.CmdSalirF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(50, 54)
        Me.CmdSalirF.TabIndex = 623
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'CmdNuevoF
        '
        Me.CmdNuevoF.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoF.Location = New System.Drawing.Point(119, 11)
        Me.CmdNuevoF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdNuevoF.Name = "CmdNuevoF"
        Me.CmdNuevoF.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevoF.TabIndex = 621
        Me.CmdNuevoF.TabStop = False
        Me.CmdNuevoF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdNuevoF.ThemeName = "Aqua"
        '
        'CmdGuardarF
        '
        Me.CmdGuardarF.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarF.Location = New System.Drawing.Point(173, 11)
        Me.CmdGuardarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarF.Name = "CmdGuardarF"
        Me.CmdGuardarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarF.TabIndex = 624
        Me.CmdGuardarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardarF.ThemeName = "Aqua"
        '
        'CmdBuscarFact
        '
        Me.CmdBuscarFact.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarFact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarFact.Location = New System.Drawing.Point(65, 11)
        Me.CmdBuscarFact.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarFact.TabIndex = 622
        Me.CmdBuscarFact.TabStop = False
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.Tabla)
        Me.RadPanel2.Location = New System.Drawing.Point(857, 31)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(271, 166)
        Me.RadPanel2.TabIndex = 2
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToDeleteRows = False
        Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.Tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Val, Me.Des})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 0)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.ReadOnly = True
        Me.Tabla.Size = New System.Drawing.Size(271, 166)
        Me.Tabla.TabIndex = 709
        '
        'Val
        '
        Me.Val.HeaderText = "Valor"
        Me.Val.Name = "Val"
        Me.Val.ReadOnly = True
        Me.Val.Width = 69
        '
        'Des
        '
        Me.Des.HeaderText = "Descripcion"
        Me.Des.Name = "Des"
        Me.Des.ReadOnly = True
        Me.Des.Width = 108
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaImportar.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Mat, Me.Nombres, Me.Appaterno, Me.Ap_materno, Me.RegistroPatronal, Me.DigVerificadorRP, Me.NoIMSS, Me.DigVerifIMSS, Me.SalarioBase, Me.TipoTrabajador, Me.TipoSalario, Me.SemJornadaReducida, Me.UnidadMedicinaFamiliar, Me.Guia, Me.CURP, Me.Fechaalta, Me.Fecha_Baja})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TablaImportar.DefaultCellStyle = DataGridViewCellStyle5
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 233)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaImportar.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(1268, 303)
        Me.TablaImportar.TabIndex = 3
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        '
        'Id
        '
        Me.Id.HeaderText = "Id_Persona_Cliente"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        '
        'Mat
        '
        Me.Mat.HeaderText = "Matricula"
        Me.Mat.Name = "Mat"
        '
        'Nombres
        '
        Me.Nombres.HeaderText = "Nombres"
        Me.Nombres.Name = "Nombres"
        '
        'Appaterno
        '
        Me.Appaterno.HeaderText = "Ap Paterno"
        Me.Appaterno.Name = "Appaterno"
        '
        'Ap_materno
        '
        Me.Ap_materno.HeaderText = "Ap Materno"
        Me.Ap_materno.Name = "Ap_materno"
        '
        'RegistroPatronal
        '
        Me.RegistroPatronal.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.RegistroPatronal.HeaderText = "Registro_Patronal"
        Me.RegistroPatronal.Name = "RegistroPatronal"
        Me.RegistroPatronal.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RegistroPatronal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DigVerificadorRP
        '
        Me.DigVerificadorRP.HeaderText = "Dig_Verificador_RP"
        Me.DigVerificadorRP.Name = "DigVerificadorRP"
        '
        'NoIMSS
        '
        Me.NoIMSS.HeaderText = "No_IMSS"
        Me.NoIMSS.Name = "NoIMSS"
        '
        'DigVerifIMSS
        '
        Me.DigVerifIMSS.HeaderText = "Dig_Verif_IMSS"
        Me.DigVerifIMSS.Name = "DigVerifIMSS"
        '
        'SalarioBase
        '
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.SalarioBase.DefaultCellStyle = DataGridViewCellStyle2
        Me.SalarioBase.HeaderText = "Salario_Base"
        Me.SalarioBase.Name = "SalarioBase"
        '
        'TipoTrabajador
        '
        Me.TipoTrabajador.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.TipoTrabajador.HeaderText = "Tipo_Trabajador"
        Me.TipoTrabajador.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.TipoTrabajador.Name = "TipoTrabajador"
        Me.TipoTrabajador.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TipoTrabajador.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TipoSalario
        '
        Me.TipoSalario.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.TipoSalario.HeaderText = "Tipo_Salario"
        Me.TipoSalario.Items.AddRange(New Object() {"0", "1", "2"})
        Me.TipoSalario.Name = "TipoSalario"
        Me.TipoSalario.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TipoSalario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'SemJornadaReducida
        '
        Me.SemJornadaReducida.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.SemJornadaReducida.HeaderText = "Sem_Jornada_Reducida"
        Me.SemJornadaReducida.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6"})
        Me.SemJornadaReducida.Name = "SemJornadaReducida"
        Me.SemJornadaReducida.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SemJornadaReducida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'UnidadMedicinaFamiliar
        '
        Me.UnidadMedicinaFamiliar.HeaderText = "Unidad_Medicina_Familiar"
        Me.UnidadMedicinaFamiliar.Name = "UnidadMedicinaFamiliar"
        '
        'Guia
        '
        Me.Guia.HeaderText = "Guia"
        Me.Guia.Name = "Guia"
        '
        'CURP
        '
        Me.CURP.HeaderText = "CURP"
        Me.CURP.Name = "CURP"
        '
        'Fechaalta
        '
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Fechaalta.DefaultCellStyle = DataGridViewCellStyle3
        Me.Fechaalta.HeaderText = "Fecha_alta"
        Me.Fechaalta.Name = "Fechaalta"
        '
        'Fecha_Baja
        '
        DataGridViewCellStyle4.Format = "d"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Fecha_Baja.DefaultCellStyle = DataGridViewCellStyle4
        Me.Fecha_Baja.HeaderText = "Fecha_Baja"
        Me.Fecha_Baja.Name = "Fecha_Baja"
        '
        'LstTexto
        '
        Me.LstTexto.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstTexto.Location = New System.Drawing.Point(15, 161)
        Me.LstTexto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstTexto.Name = "LstTexto"
        Me.LstTexto.SelectItem = ""
        Me.LstTexto.SelectText = ""
        Me.LstTexto.Size = New System.Drawing.Size(424, 36)
        Me.LstTexto.TabIndex = 637
        '
        'LstSubDelegacion
        '
        Me.LstSubDelegacion.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstSubDelegacion.Location = New System.Drawing.Point(462, 161)
        Me.LstSubDelegacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstSubDelegacion.Name = "LstSubDelegacion"
        Me.LstSubDelegacion.SelectItem = ""
        Me.LstSubDelegacion.SelectText = ""
        Me.LstSubDelegacion.Size = New System.Drawing.Size(370, 36)
        Me.LstSubDelegacion.TabIndex = 634
        '
        'LstDelegacion
        '
        Me.LstDelegacion.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstDelegacion.Location = New System.Drawing.Point(461, 92)
        Me.LstDelegacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstDelegacion.Name = "LstDelegacion"
        Me.LstDelegacion.SelectItem = ""
        Me.LstDelegacion.SelectText = ""
        Me.LstDelegacion.Size = New System.Drawing.Size(370, 36)
        Me.LstDelegacion.TabIndex = 632
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(460, 31)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 626
        '
        'Control_Masivo_Personal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1268, 536)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel2)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Control_Masivo_Personal"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control Masivo Personal"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdMovimientos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdAsignaDelegacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdMasivo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents LstTexto As Listas
	Friend WithEvents Label5 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents LstSubDelegacion As Listas
	Friend WithEvents Label2 As Label
	Friend WithEvents LstDelegacion As Listas
	Friend WithEvents Label1 As Label
	Friend WithEvents CmdEliminarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdMovimientos As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdAsignaDelegacion As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdMasivo As Telerik.WinControls.UI.RadButton
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
	Friend WithEvents TxtFiltro As Telerik.WinControls.UI.RadTextBox
	Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents Tabla As DataGridView
	Friend WithEvents Val As DataGridViewTextBoxColumn
	Friend WithEvents Des As DataGridViewTextBoxColumn
	Friend WithEvents TablaImportar As DataGridView
    Friend WithEvents lblRegistros As Label
    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents Ayuda As ToolTip
    Friend WithEvents LblFiltro As Label
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents Mat As DataGridViewTextBoxColumn
    Friend WithEvents Nombres As DataGridViewTextBoxColumn
    Friend WithEvents Appaterno As DataGridViewTextBoxColumn
    Friend WithEvents Ap_materno As DataGridViewTextBoxColumn
    Friend WithEvents RegistroPatronal As DataGridViewComboBoxColumn
    Friend WithEvents DigVerificadorRP As DataGridViewTextBoxColumn
    Friend WithEvents NoIMSS As DataGridViewTextBoxColumn
    Friend WithEvents DigVerifIMSS As DataGridViewTextBoxColumn
    Friend WithEvents SalarioBase As DataGridViewTextBoxColumn
    Friend WithEvents TipoTrabajador As DataGridViewComboBoxColumn
    Friend WithEvents TipoSalario As DataGridViewComboBoxColumn
    Friend WithEvents SemJornadaReducida As DataGridViewComboBoxColumn
    Friend WithEvents UnidadMedicinaFamiliar As DataGridViewTextBoxColumn
    Friend WithEvents Guia As DataGridViewTextBoxColumn
    Friend WithEvents CURP As DataGridViewTextBoxColumn
    Friend WithEvents Fechaalta As DataGridViewTextBoxColumn
    Friend WithEvents Fecha_Baja As DataGridViewTextBoxColumn
End Class

