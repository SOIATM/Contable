<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Importar_Polizas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Importar_Polizas))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lblRegistros = New System.Windows.Forms.Label()
        Me.Barra = New Telerik.WinControls.UI.RadProgressBar()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdexcel = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.anio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.numpol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DCE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RFCCE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NCheque = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Banco = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BancoD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CtaD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.imp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IRS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NomCta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fmov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpChe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRIVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRISR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mV = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TXT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Largo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdexcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.lblRegistros)
        Me.RadPanel1.Controls.Add(Me.Barra)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.cmdexcel)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdImportar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(929, 100)
        Me.RadPanel1.TabIndex = 0
        '
        'lblRegistros
        '
        Me.lblRegistros.AutoSize = True
        Me.lblRegistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistros.Location = New System.Drawing.Point(686, 9)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.Size = New System.Drawing.Size(112, 18)
        Me.lblRegistros.TabIndex = 21
        Me.lblRegistros.Text = "Total de registros:"
        '
        'Barra
        '
        Me.Barra.Location = New System.Drawing.Point(7, 61)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(256, 24)
        Me.Barra.TabIndex = 590
        Me.Barra.Text = " "
        Me.Barra.ThemeName = "Material"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(297, 30)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 589
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(294, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 588
        Me.Label3.Text = "Empresa:"
        '
        'cmdexcel
        '
        Me.cmdexcel.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.cmdexcel.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdexcel.Location = New System.Drawing.Point(218, 2)
        Me.cmdexcel.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdexcel.Name = "cmdexcel"
        Me.cmdexcel.Size = New System.Drawing.Size(50, 54)
        Me.cmdexcel.TabIndex = 586
        Me.cmdexcel.TabStop = False
        Me.cmdexcel.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.cmdexcel.ThemeName = "Aqua"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(56, 2)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 584
        Me.CmdLimpiar.TabStop = False
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(164, 2)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 585
        Me.Cmd_Procesar.TabStop = False
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(2, 2)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 586
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.EXPORTAR_TXT
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(110, 2)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 587
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.anio, Me.mes, Me.tipo, Me.numpol, Me.dia, Me.DCE, Me.RFCCE, Me.NCheque, Me.Banco, Me.Cta, Me.des, Me.Mov, Me.CtaO, Me.BancoD, Me.CtaD, Me.imp, Me.PA, Me.IRS, Me.NomCta, Me.Fmov, Me.ImpFactura, Me.ff, Me.Mone, Me.ImpChe, Me.NC, Me.PRIVA, Me.PRISR, Me.PRX, Me.PRY, Me.TC, Me.mV, Me.TXT, Me.Largo, Me.T, Me.Pol})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 100)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.ReadOnly = True
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(929, 378)
        Me.TablaImportar.TabIndex = 1
        '
        'anio
        '
        Me.anio.HeaderText = "Año_Poliza"
        Me.anio.Name = "anio"
        Me.anio.ReadOnly = True
        Me.anio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'mes
        '
        Me.mes.HeaderText = "Mes_Poliza"
        Me.mes.Name = "mes"
        Me.mes.ReadOnly = True
        Me.mes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'tipo
        '
        Me.tipo.HeaderText = "Tipo_Poliza"
        Me.tipo.Name = "tipo"
        Me.tipo.ReadOnly = True
        Me.tipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'numpol
        '
        Me.numpol.HeaderText = "Num_Poliza"
        Me.numpol.Name = "numpol"
        Me.numpol.ReadOnly = True
        Me.numpol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dia
        '
        Me.dia.HeaderText = "Dia_Poliza"
        Me.dia.Name = "dia"
        Me.dia.ReadOnly = True
        Me.dia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DCE
        '
        Me.DCE.HeaderText = "Detalle_CE"
        Me.DCE.Name = "DCE"
        Me.DCE.ReadOnly = True
        Me.DCE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'RFCCE
        '
        Me.RFCCE.HeaderText = "RFC_CE"
        Me.RFCCE.Name = "RFCCE"
        Me.RFCCE.ReadOnly = True
        Me.RFCCE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'NCheque
        '
        Me.NCheque.HeaderText = "No_Cheque"
        Me.NCheque.Name = "NCheque"
        Me.NCheque.ReadOnly = True
        Me.NCheque.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Banco
        '
        Me.Banco.HeaderText = "No_Banco"
        Me.Banco.Name = "Banco"
        Me.Banco.ReadOnly = True
        Me.Banco.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Cta
        '
        Me.Cta.HeaderText = "Cuenta"
        Me.Cta.Name = "Cta"
        Me.Cta.ReadOnly = True
        Me.Cta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'des
        '
        Me.des.HeaderText = "Descripcion"
        Me.des.Name = "des"
        Me.des.ReadOnly = True
        Me.des.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Mov
        '
        Me.Mov.HeaderText = "Tipo_movimiento"
        Me.Mov.Name = "Mov"
        Me.Mov.ReadOnly = True
        Me.Mov.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CtaO
        '
        Me.CtaO.HeaderText = "Cuenta_Origen"
        Me.CtaO.Name = "CtaO"
        Me.CtaO.ReadOnly = True
        Me.CtaO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BancoD
        '
        Me.BancoD.HeaderText = "Banco_Destino"
        Me.BancoD.Name = "BancoD"
        Me.BancoD.ReadOnly = True
        '
        'CtaD
        '
        Me.CtaD.HeaderText = "Cuenta_Destino"
        Me.CtaD.Name = "CtaD"
        Me.CtaD.ReadOnly = True
        '
        'imp
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = "0"
        Me.imp.DefaultCellStyle = DataGridViewCellStyle1
        Me.imp.HeaderText = "Importe"
        Me.imp.Name = "imp"
        Me.imp.ReadOnly = True
        Me.imp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PA
        '
        Me.PA.HeaderText = "P_Aplicar"
        Me.PA.Name = "PA"
        Me.PA.ReadOnly = True
        Me.PA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'IRS
        '
        Me.IRS.HeaderText = "Imp_Reg_Simplificado"
        Me.IRS.Name = "IRS"
        Me.IRS.ReadOnly = True
        Me.IRS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'NomCta
        '
        Me.NomCta.HeaderText = "Nombre_Cuenta"
        Me.NomCta.Name = "NomCta"
        Me.NomCta.ReadOnly = True
        Me.NomCta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Fmov
        '
        Me.Fmov.HeaderText = "Fecha_Mov"
        Me.Fmov.Name = "Fmov"
        Me.Fmov.ReadOnly = True
        Me.Fmov.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ImpFactura
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.ImpFactura.DefaultCellStyle = DataGridViewCellStyle2
        Me.ImpFactura.HeaderText = "Importe_Factura"
        Me.ImpFactura.Name = "ImpFactura"
        Me.ImpFactura.ReadOnly = True
        Me.ImpFactura.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ff
        '
        Me.ff.HeaderText = "Folio_Fiscal"
        Me.ff.Name = "ff"
        Me.ff.ReadOnly = True
        Me.ff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Mone
        '
        Me.Mone.HeaderText = "Moneda"
        Me.Mone.Name = "Mone"
        Me.Mone.ReadOnly = True
        Me.Mone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ImpChe
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.ImpChe.DefaultCellStyle = DataGridViewCellStyle3
        Me.ImpChe.HeaderText = "Importe_Cheque"
        Me.ImpChe.Name = "ImpChe"
        Me.ImpChe.ReadOnly = True
        Me.ImpChe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'NC
        '
        Me.NC.HeaderText = "No_Comprobante"
        Me.NC.Name = "NC"
        Me.NC.ReadOnly = True
        Me.NC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PRIVA
        '
        Me.PRIVA.HeaderText = "Porc_Ret_IVA"
        Me.PRIVA.Name = "PRIVA"
        Me.PRIVA.ReadOnly = True
        Me.PRIVA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PRISR
        '
        Me.PRISR.HeaderText = "Por_Ret_ISR"
        Me.PRISR.Name = "PRISR"
        Me.PRISR.ReadOnly = True
        Me.PRISR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PRX
        '
        Me.PRX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.PRX.HeaderText = "Porc_Ret_X"
        Me.PRX.Name = "PRX"
        Me.PRX.ReadOnly = True
        Me.PRX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PRY
        '
        Me.PRY.HeaderText = "Por_Ret_Y"
        Me.PRY.Name = "PRY"
        Me.PRY.ReadOnly = True
        Me.PRY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TC
        '
        Me.TC.HeaderText = "Tipo_Comprobante"
        Me.TC.Name = "TC"
        Me.TC.ReadOnly = True
        Me.TC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'mV
        '
        Me.mV.HeaderText = "Mov"
        Me.mV.Name = "mV"
        Me.mV.ReadOnly = True
        Me.mV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TXT
        '
        Me.TXT.HeaderText = "Textos"
        Me.TXT.Name = "TXT"
        Me.TXT.ReadOnly = True
        Me.TXT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Largo
        '
        Me.Largo.HeaderText = "Longitud"
        Me.Largo.Name = "Largo"
        Me.Largo.ReadOnly = True
        Me.Largo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'T
        '
        Me.T.HeaderText = "Tipo"
        Me.T.Name = "T"
        Me.T.ReadOnly = True
        Me.T.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.T.Visible = False
        '
        'Pol
        '
        Me.Pol.HeaderText = "Poliza_Sistema"
        Me.Pol.Name = "Pol"
        Me.Pol.ReadOnly = True
        Me.Pol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Pol.Visible = False
        '
        'Ayuda
        '
        Me.Ayuda.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Ayuda.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'Importar_Polizas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(929, 478)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Importar_Polizas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Importar Pólizas"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdexcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents cmdexcel As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Barra As Telerik.WinControls.UI.RadProgressBar
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents lblRegistros As Label
	Friend WithEvents TablaImportar As DataGridView
	Friend WithEvents anio As DataGridViewTextBoxColumn
	Friend WithEvents mes As DataGridViewTextBoxColumn
	Friend WithEvents tipo As DataGridViewTextBoxColumn
	Friend WithEvents numpol As DataGridViewTextBoxColumn
	Friend WithEvents dia As DataGridViewTextBoxColumn
	Friend WithEvents DCE As DataGridViewTextBoxColumn
	Friend WithEvents RFCCE As DataGridViewTextBoxColumn
	Friend WithEvents NCheque As DataGridViewTextBoxColumn
	Friend WithEvents Banco As DataGridViewTextBoxColumn
	Friend WithEvents Cta As DataGridViewTextBoxColumn
	Friend WithEvents des As DataGridViewTextBoxColumn
	Friend WithEvents Mov As DataGridViewTextBoxColumn
	Friend WithEvents CtaO As DataGridViewTextBoxColumn
	Friend WithEvents BancoD As DataGridViewTextBoxColumn
	Friend WithEvents CtaD As DataGridViewTextBoxColumn
	Friend WithEvents imp As DataGridViewTextBoxColumn
	Friend WithEvents PA As DataGridViewTextBoxColumn
	Friend WithEvents IRS As DataGridViewTextBoxColumn
	Friend WithEvents NomCta As DataGridViewTextBoxColumn
	Friend WithEvents Fmov As DataGridViewTextBoxColumn
	Friend WithEvents ImpFactura As DataGridViewTextBoxColumn
	Friend WithEvents ff As DataGridViewTextBoxColumn
	Friend WithEvents Mone As DataGridViewTextBoxColumn
	Friend WithEvents ImpChe As DataGridViewTextBoxColumn
	Friend WithEvents NC As DataGridViewTextBoxColumn
	Friend WithEvents PRIVA As DataGridViewTextBoxColumn
	Friend WithEvents PRISR As DataGridViewTextBoxColumn
	Friend WithEvents PRX As DataGridViewTextBoxColumn
	Friend WithEvents PRY As DataGridViewTextBoxColumn
	Friend WithEvents TC As DataGridViewTextBoxColumn
	Friend WithEvents mV As DataGridViewTextBoxColumn
	Friend WithEvents TXT As DataGridViewTextBoxColumn
	Friend WithEvents Largo As DataGridViewTextBoxColumn
	Friend WithEvents T As DataGridViewTextBoxColumn
	Friend WithEvents Pol As DataGridViewTextBoxColumn
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Ayuda As ToolTip
End Class

