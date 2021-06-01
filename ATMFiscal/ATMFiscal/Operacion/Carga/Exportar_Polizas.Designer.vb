<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Exportar_Polizas
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Exportar_Polizas))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadEmitidas = New System.Windows.Forms.RadioButton()
        Me.RadRecibidas = New System.Windows.Forms.RadioButton()
        Me.RadSistema = New System.Windows.Forms.RadioButton()
        Me.lblRegistros = New System.Windows.Forms.Label()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboMes = New Telerik.WinControls.UI.RadDropDownList()
        Me.comboAño = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadFechas = New System.Windows.Forms.RadioButton()
        Me.RadMensual = New System.Windows.Forms.RadioButton()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Barra = New Telerik.WinControls.UI.RadProgressBar()
        Me.CmdAbrir = New Telerik.WinControls.UI.RadButton()
        Me.CmdUnir = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdGenerar = New Telerik.WinControls.UI.RadButton()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.An = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tpol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Np = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dce = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RFCce = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nb = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Imp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NCta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpCh = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRIVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRISR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.M = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PolC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.DT_fechaf = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.DT_fechai = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboAño, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdAbrir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdUnir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGenerar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DT_fechaf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DT_fechai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.AutoScroll = True
        Me.RadPanel1.BackColor = System.Drawing.Color.CadetBlue
        Me.RadPanel1.Controls.Add(Me.RadGroupBox2)
        Me.RadPanel1.Controls.Add(Me.RadSistema)
        Me.RadPanel1.Controls.Add(Me.lblRegistros)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.Barra)
        Me.RadPanel1.Controls.Add(Me.CmdAbrir)
        Me.RadPanel1.Controls.Add(Me.CmdUnir)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdGenerar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1456, 169)
        Me.RadPanel1.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox2.Controls.Add(Me.RadEmitidas)
        Me.RadGroupBox2.Controls.Add(Me.RadRecibidas)
        Me.RadGroupBox2.HeaderText = "Tipo"
        Me.RadGroupBox2.Location = New System.Drawing.Point(531, 76)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(204, 71)
        Me.RadGroupBox2.TabIndex = 605
        Me.RadGroupBox2.Text = "Tipo"
        Me.RadGroupBox2.ThemeName = "Material"
        '
        'RadEmitidas
        '
        Me.RadEmitidas.AutoSize = True
        Me.RadEmitidas.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadEmitidas.ForeColor = System.Drawing.Color.Navy
        Me.RadEmitidas.Location = New System.Drawing.Point(113, 39)
        Me.RadEmitidas.Name = "RadEmitidas"
        Me.RadEmitidas.Size = New System.Drawing.Size(79, 22)
        Me.RadEmitidas.TabIndex = 601
        Me.RadEmitidas.Text = "Emitidas"
        Me.RadEmitidas.UseVisualStyleBackColor = True
        '
        'RadRecibidas
        '
        Me.RadRecibidas.AutoSize = True
        Me.RadRecibidas.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadRecibidas.Checked = True
        Me.RadRecibidas.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadRecibidas.ForeColor = System.Drawing.Color.DarkGreen
        Me.RadRecibidas.Location = New System.Drawing.Point(5, 39)
        Me.RadRecibidas.Name = "RadRecibidas"
        Me.RadRecibidas.Size = New System.Drawing.Size(85, 22)
        Me.RadRecibidas.TabIndex = 603
        Me.RadRecibidas.TabStop = True
        Me.RadRecibidas.Text = "Recibidas"
        Me.RadRecibidas.UseVisualStyleBackColor = False
        '
        'RadSistema
        '
        Me.RadSistema.AutoSize = True
        Me.RadSistema.Checked = True
        Me.RadSistema.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold)
        Me.RadSistema.Location = New System.Drawing.Point(339, 102)
        Me.RadSistema.Name = "RadSistema"
        Me.RadSistema.Size = New System.Drawing.Size(167, 25)
        Me.RadSistema.TabIndex = 602
        Me.RadSistema.TabStop = True
        Me.RadSistema.Text = "Desde / Sistema"
        Me.RadSistema.UseVisualStyleBackColor = True
        '
        'lblRegistros
        '
        Me.lblRegistros.AutoSize = True
        Me.lblRegistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistros.Location = New System.Drawing.Point(3, 112)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.Size = New System.Drawing.Size(112, 18)
        Me.lblRegistros.TabIndex = 597
        Me.lblRegistros.Text = "Total de registros:"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.DT_fechaf)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.DT_fechai)
        Me.RadGroupBox1.Controls.Add(Me.Label6)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.Label7)
        Me.RadGroupBox1.Controls.Add(Me.ComboMes)
        Me.RadGroupBox1.Controls.Add(Me.comboAño)
        Me.RadGroupBox1.Controls.Add(Me.RadFechas)
        Me.RadGroupBox1.Controls.Add(Me.RadMensual)
        Me.RadGroupBox1.HeaderText = "Periodo Mensual:     "
        Me.RadGroupBox1.Location = New System.Drawing.Point(772, 9)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(675, 145)
        Me.RadGroupBox1.TabIndex = 596
        Me.RadGroupBox1.Text = "Periodo Mensual:     "
        Me.RadGroupBox1.ThemeName = "Material"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(275, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 18)
        Me.Label1.TabIndex = 597
        Me.Label1.Text = "Mes:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(134, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 18)
        Me.Label2.TabIndex = 598
        Me.Label2.Text = "Año:"
        '
        'ComboMes
        '
        Me.ComboMes.Location = New System.Drawing.Point(278, 29)
        Me.ComboMes.Name = "ComboMes"
        Me.ComboMes.Size = New System.Drawing.Size(119, 36)
        Me.ComboMes.TabIndex = 3
        Me.ComboMes.Text = " "
        Me.ComboMes.ThemeName = "Material"
        '
        'comboAño
        '
        Me.comboAño.Location = New System.Drawing.Point(137, 29)
        Me.comboAño.Name = "comboAño"
        Me.comboAño.Size = New System.Drawing.Size(117, 36)
        Me.comboAño.TabIndex = 2
        Me.comboAño.Text = " "
        Me.comboAño.ThemeName = "Material"
        '
        'RadFechas
        '
        Me.RadFechas.AutoSize = True
        Me.RadFechas.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadFechas.Location = New System.Drawing.Point(5, 94)
        Me.RadFechas.Name = "RadFechas"
        Me.RadFechas.Size = New System.Drawing.Size(113, 22)
        Me.RadFechas.TabIndex = 1
        Me.RadFechas.Text = "Fechas FI / FF"
        Me.RadFechas.UseVisualStyleBackColor = True
        '
        'RadMensual
        '
        Me.RadMensual.AutoSize = True
        Me.RadMensual.Checked = True
        Me.RadMensual.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMensual.Location = New System.Drawing.Point(5, 39)
        Me.RadMensual.Name = "RadMensual"
        Me.RadMensual.Size = New System.Drawing.Size(123, 22)
        Me.RadMensual.TabIndex = 0
        Me.RadMensual.TabStop = True
        Me.RadMensual.Text = "Mensual / Anual"
        Me.RadMensual.UseVisualStyleBackColor = True
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(340, 33)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 595
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(336, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 594
        Me.Label3.Text = "Empresa:"
        '
        'Barra
        '
        Me.Barra.Location = New System.Drawing.Point(3, 82)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(319, 24)
        Me.Barra.TabIndex = 593
        Me.Barra.Text = " "
        Me.Barra.ThemeName = "Material"
        '
        'CmdAbrir
        '
        Me.CmdAbrir.Image = Global.ATMFiscal.My.Resources.Resources.documentos
        Me.CmdAbrir.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdAbrir.Location = New System.Drawing.Point(218, 22)
        Me.CmdAbrir.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdAbrir.Name = "CmdAbrir"
        Me.CmdAbrir.Size = New System.Drawing.Size(50, 54)
        Me.CmdAbrir.TabIndex = 590
        Me.CmdAbrir.TabStop = False
        Me.CmdAbrir.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdAbrir.ThemeName = "Aqua"
        '
        'CmdUnir
        '
        Me.CmdUnir.Image = Global.ATMFiscal.My.Resources.Resources.BANCOSC
        Me.CmdUnir.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdUnir.Location = New System.Drawing.Point(272, 22)
        Me.CmdUnir.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdUnir.Name = "CmdUnir"
        Me.CmdUnir.Size = New System.Drawing.Size(50, 54)
        Me.CmdUnir.TabIndex = 591
        Me.CmdUnir.TabStop = False
        Me.CmdUnir.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdUnir.ThemeName = "Aqua"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(56, 22)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 588
        Me.CmdLimpiar.TabStop = False
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(110, 22)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 589
        Me.Cmd_Procesar.TabStop = False
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(2, 22)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 591
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdGenerar
        '
        Me.CmdGenerar.Image = Global.ATMFiscal.My.Resources.Resources.EXPORTAR_TXT
        Me.CmdGenerar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGenerar.Location = New System.Drawing.Point(164, 22)
        Me.CmdGenerar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGenerar.Name = "CmdGenerar"
        Me.CmdGenerar.Size = New System.Drawing.Size(50, 54)
        Me.CmdGenerar.TabIndex = 592
        Me.CmdGenerar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGenerar.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.An, Me.MS, Me.Tpol, Me.Np, Me.Dp, Me.Dce, Me.RFCce, Me.Nch, Me.Nb, Me.Cta, Me.Des, Me.Tm, Me.CtaO, Me.BD, Me.CtaD, Me.Imp, Me.PA, Me.isr, Me.NCta, Me.FM, Me.ImpF, Me.Ff, Me.Mone, Me.ImpCh, Me.NC, Me.PRIVA, Me.PRISR, Me.PRX, Me.PRY, Me.TC, Me.M, Me.PolC})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 169)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.ReadOnly = True
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(1456, 302)
        Me.TablaImportar.TabIndex = 1
        '
        'An
        '
        Me.An.HeaderText = "Año_Poliza"
        Me.An.Name = "An"
        Me.An.ReadOnly = True
        '
        'MS
        '
        Me.MS.HeaderText = "Mes_Poliza"
        Me.MS.Name = "MS"
        Me.MS.ReadOnly = True
        '
        'Tpol
        '
        Me.Tpol.HeaderText = "Tipo_Poliza"
        Me.Tpol.Name = "Tpol"
        Me.Tpol.ReadOnly = True
        '
        'Np
        '
        Me.Np.HeaderText = "Num_Poliza"
        Me.Np.Name = "Np"
        Me.Np.ReadOnly = True
        '
        'Dp
        '
        Me.Dp.HeaderText = "Dia_Poliza"
        Me.Dp.Name = "Dp"
        Me.Dp.ReadOnly = True
        '
        'Dce
        '
        DataGridViewCellStyle1.NullValue = " "
        Me.Dce.DefaultCellStyle = DataGridViewCellStyle1
        Me.Dce.HeaderText = "Detalle_CE"
        Me.Dce.Name = "Dce"
        Me.Dce.ReadOnly = True
        '
        'RFCce
        '
        DataGridViewCellStyle2.NullValue = Nothing
        Me.RFCce.DefaultCellStyle = DataGridViewCellStyle2
        Me.RFCce.HeaderText = "RFC_CE"
        Me.RFCce.Name = "RFCce"
        Me.RFCce.ReadOnly = True
        '
        'Nch
        '
        Me.Nch.HeaderText = "No_Cheque"
        Me.Nch.Name = "Nch"
        Me.Nch.ReadOnly = True
        '
        'Nb
        '
        Me.Nb.HeaderText = "No_Banco"
        Me.Nb.Name = "Nb"
        Me.Nb.ReadOnly = True
        '
        'Cta
        '
        Me.Cta.HeaderText = "Cuenta"
        Me.Cta.Name = "Cta"
        Me.Cta.ReadOnly = True
        '
        'Des
        '
        Me.Des.HeaderText = "Descripcion"
        Me.Des.Name = "Des"
        Me.Des.ReadOnly = True
        '
        'Tm
        '
        Me.Tm.HeaderText = "Tipo_movimiento"
        Me.Tm.Name = "Tm"
        Me.Tm.ReadOnly = True
        '
        'CtaO
        '
        Me.CtaO.HeaderText = "Cuenta_Origen"
        Me.CtaO.Name = "CtaO"
        Me.CtaO.ReadOnly = True
        '
        'BD
        '
        Me.BD.HeaderText = "Banco_Destino"
        Me.BD.Name = "BD"
        Me.BD.ReadOnly = True
        '
        'CtaD
        '
        Me.CtaD.HeaderText = "Cuenta_Destino"
        Me.CtaD.Name = "CtaD"
        Me.CtaD.ReadOnly = True
        '
        'Imp
        '
        DataGridViewCellStyle3.Format = "0.00"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Imp.DefaultCellStyle = DataGridViewCellStyle3
        Me.Imp.HeaderText = "Importe"
        Me.Imp.Name = "Imp"
        Me.Imp.ReadOnly = True
        '
        'PA
        '
        Me.PA.HeaderText = "P_Aplicar"
        Me.PA.Name = "PA"
        Me.PA.ReadOnly = True
        '
        'isr
        '
        Me.isr.HeaderText = "Imp_Reg_Simplificado"
        Me.isr.Name = "isr"
        Me.isr.ReadOnly = True
        '
        'NCta
        '
        Me.NCta.HeaderText = "Nombre_Cuenta"
        Me.NCta.Name = "NCta"
        Me.NCta.ReadOnly = True
        '
        'FM
        '
        Me.FM.HeaderText = "Fecha_Mov"
        Me.FM.Name = "FM"
        Me.FM.ReadOnly = True
        '
        'ImpF
        '
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.ImpF.DefaultCellStyle = DataGridViewCellStyle4
        Me.ImpF.HeaderText = "Importe_Factura"
        Me.ImpF.Name = "ImpF"
        Me.ImpF.ReadOnly = True
        '
        'Ff
        '
        Me.Ff.HeaderText = "Folio_Fiscal"
        Me.Ff.Name = "Ff"
        Me.Ff.ReadOnly = True
        '
        'Mone
        '
        Me.Mone.HeaderText = "Moneda"
        Me.Mone.Name = "Mone"
        Me.Mone.ReadOnly = True
        '
        'ImpCh
        '
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.ImpCh.DefaultCellStyle = DataGridViewCellStyle5
        Me.ImpCh.HeaderText = "Importe_Cheque"
        Me.ImpCh.Name = "ImpCh"
        Me.ImpCh.ReadOnly = True
        '
        'NC
        '
        Me.NC.HeaderText = "No_Comprobante"
        Me.NC.Name = "NC"
        Me.NC.ReadOnly = True
        '
        'PRIVA
        '
        Me.PRIVA.HeaderText = "Porc_Ret_IVA"
        Me.PRIVA.Name = "PRIVA"
        Me.PRIVA.ReadOnly = True
        '
        'PRISR
        '
        Me.PRISR.HeaderText = "Por_Ret_ISR"
        Me.PRISR.Name = "PRISR"
        Me.PRISR.ReadOnly = True
        '
        'PRX
        '
        Me.PRX.HeaderText = "Porc_Ret_X"
        Me.PRX.Name = "PRX"
        Me.PRX.ReadOnly = True
        '
        'PRY
        '
        Me.PRY.HeaderText = "Por_Ret_Y"
        Me.PRY.Name = "PRY"
        Me.PRY.ReadOnly = True
        '
        'TC
        '
        Me.TC.HeaderText = "Tipo_Comprobante"
        Me.TC.Name = "TC"
        Me.TC.ReadOnly = True
        '
        'M
        '
        Me.M.HeaderText = "Mov"
        Me.M.Name = "M"
        Me.M.ReadOnly = True
        '
        'PolC
        '
        Me.PolC.HeaderText = "Poliza_Calculada"
        Me.PolC.Name = "PolC"
        Me.PolC.ReadOnly = True
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'DT_fechaf
        '
        Me.DT_fechaf.AutoSize = False
        Me.DT_fechaf.CalendarSize = New System.Drawing.Size(290, 320)
        Me.DT_fechaf.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DT_fechaf.Location = New System.Drawing.Point(403, 92)
        Me.DT_fechaf.Name = "DT_fechaf"
        Me.DT_fechaf.Size = New System.Drawing.Size(259, 41)
        Me.DT_fechaf.TabIndex = 645
        Me.DT_fechaf.TabStop = False
        Me.DT_fechaf.Text = "viernes, 19 de febrero de 2021"
        Me.DT_fechaf.ThemeName = "MaterialBlueGrey"
        Me.DT_fechaf.Value = New Date(2021, 2, 19, 12, 2, 23, 431)
        '
        'DT_fechai
        '
        Me.DT_fechai.AutoSize = False
        Me.DT_fechai.CalendarSize = New System.Drawing.Size(290, 320)
        Me.DT_fechai.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DT_fechai.Location = New System.Drawing.Point(138, 92)
        Me.DT_fechai.Name = "DT_fechai"
        Me.DT_fechai.Size = New System.Drawing.Size(259, 41)
        Me.DT_fechai.TabIndex = 644
        Me.DT_fechai.TabStop = False
        Me.DT_fechai.Text = "viernes, 19 de febrero de 2021"
        Me.DT_fechai.ThemeName = "MaterialBlueGrey"
        Me.DT_fechai.Value = New Date(2021, 2, 19, 12, 2, 23, 431)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(486, 71)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 18)
        Me.Label6.TabIndex = 643
        Me.Label6.Text = "Fecha Final:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(216, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 18)
        Me.Label7.TabIndex = 642
        Me.Label7.Text = "Fecha Inicial:"
        '
        'Exportar_Polizas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1456, 471)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Exportar_Polizas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Exportar Pólizas"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboAño, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdAbrir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdUnir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGenerar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DT_fechaf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DT_fechai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents Barra As Telerik.WinControls.UI.RadProgressBar
	Friend WithEvents CmdAbrir As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdUnir As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGenerar As Telerik.WinControls.UI.RadButton
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
    Friend WithEvents ComboMes As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents comboAño As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RadFechas As RadioButton
    Friend WithEvents RadMensual As RadioButton
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label3 As Label
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadEmitidas As RadioButton
    Friend WithEvents RadRecibidas As RadioButton
    Friend WithEvents RadSistema As RadioButton
    Friend WithEvents lblRegistros As Label
    Friend WithEvents TablaImportar As DataGridView
    Friend WithEvents An As DataGridViewTextBoxColumn
    Friend WithEvents MS As DataGridViewTextBoxColumn
    Friend WithEvents Tpol As DataGridViewTextBoxColumn
    Friend WithEvents Np As DataGridViewTextBoxColumn
    Friend WithEvents Dp As DataGridViewTextBoxColumn
    Friend WithEvents Dce As DataGridViewTextBoxColumn
    Friend WithEvents RFCce As DataGridViewTextBoxColumn
    Friend WithEvents Nch As DataGridViewTextBoxColumn
    Friend WithEvents Nb As DataGridViewTextBoxColumn
    Friend WithEvents Cta As DataGridViewTextBoxColumn
    Friend WithEvents Des As DataGridViewTextBoxColumn
    Friend WithEvents Tm As DataGridViewTextBoxColumn
    Friend WithEvents CtaO As DataGridViewTextBoxColumn
    Friend WithEvents BD As DataGridViewTextBoxColumn
    Friend WithEvents CtaD As DataGridViewTextBoxColumn
    Friend WithEvents Imp As DataGridViewTextBoxColumn
    Friend WithEvents PA As DataGridViewTextBoxColumn
    Friend WithEvents isr As DataGridViewTextBoxColumn
    Friend WithEvents NCta As DataGridViewTextBoxColumn
    Friend WithEvents FM As DataGridViewTextBoxColumn
    Friend WithEvents ImpF As DataGridViewTextBoxColumn
    Friend WithEvents Ff As DataGridViewTextBoxColumn
    Friend WithEvents Mone As DataGridViewTextBoxColumn
    Friend WithEvents ImpCh As DataGridViewTextBoxColumn
    Friend WithEvents NC As DataGridViewTextBoxColumn
    Friend WithEvents PRIVA As DataGridViewTextBoxColumn
    Friend WithEvents PRISR As DataGridViewTextBoxColumn
    Friend WithEvents PRX As DataGridViewTextBoxColumn
    Friend WithEvents PRY As DataGridViewTextBoxColumn
    Friend WithEvents TC As DataGridViewTextBoxColumn
    Friend WithEvents M As DataGridViewTextBoxColumn
    Friend WithEvents PolC As DataGridViewTextBoxColumn
    Friend WithEvents Ayuda As ToolTip
    Friend WithEvents DT_fechaf As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents DT_fechai As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class

