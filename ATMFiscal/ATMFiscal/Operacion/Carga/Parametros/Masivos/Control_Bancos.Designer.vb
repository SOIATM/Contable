<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Control_Bancos
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Control_Bancos))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RadTarjeta = New System.Windows.Forms.RadioButton()
        Me.RadCheque = New System.Windows.Forms.RadioButton()
        Me.RadTransf = New System.Windows.Forms.RadioButton()
        Me.DTFechaF = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.DTFechaI = New Telerik.WinControls.UI.RadMaskedEditBox()
        Me.TxtCuenta = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtAlias = New Telerik.WinControls.UI.RadTextBox()
        Me.LstCuentas = New ATMFiscal.Listas()
        Me.lstBancos = New ATMFiscal.Listas()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.TablaBancos = New ATMFiscal.Tabla_Filtro()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Tabla_detalleBancos = New ATMFiscal.tabla_detalle()
        Me.Imagenes = New System.Windows.Forms.ImageList(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.DTFechaF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTFechaI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCuenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAlias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.Imagenes
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1416, 664)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.CadetBlue
        Me.TabPage1.Controls.Add(Me.RadGroupBox1)
        Me.TabPage1.Controls.Add(Me.RadPanel1)
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 39)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1408, 621)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Bancos de la Organización"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.Label5)
        Me.RadGroupBox1.Controls.Add(Me.RadTarjeta)
        Me.RadGroupBox1.Controls.Add(Me.RadCheque)
        Me.RadGroupBox1.Controls.Add(Me.RadTransf)
        Me.RadGroupBox1.Controls.Add(Me.DTFechaF)
        Me.RadGroupBox1.Controls.Add(Me.DTFechaI)
        Me.RadGroupBox1.Controls.Add(Me.TxtCuenta)
        Me.RadGroupBox1.Controls.Add(Me.TxtAlias)
        Me.RadGroupBox1.Controls.Add(Me.LstCuentas)
        Me.RadGroupBox1.Controls.Add(Me.lstBancos)
        Me.RadGroupBox1.Controls.Add(Me.lstCliente)
        Me.RadGroupBox1.Controls.Add(Me.Label8)
        Me.RadGroupBox1.Controls.Add(Me.Label9)
        Me.RadGroupBox1.Controls.Add(Me.Label10)
        Me.RadGroupBox1.Controls.Add(Me.Label11)
        Me.RadGroupBox1.Controls.Add(Me.Label12)
        Me.RadGroupBox1.Controls.Add(Me.Label13)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic)
        Me.RadGroupBox1.HeaderText = "Parametros"
        Me.RadGroupBox1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadGroupBox1.Location = New System.Drawing.Point(19, 366)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1332, 218)
        Me.RadGroupBox1.TabIndex = 691
        Me.RadGroupBox1.Text = "Parametros"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 133)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 18)
        Me.Label5.TabIndex = 690
        Me.Label5.Text = "Alias Banco:"
        '
        'RadTarjeta
        '
        Me.RadTarjeta.AutoSize = True
        Me.RadTarjeta.Location = New System.Drawing.Point(1188, 122)
        Me.RadTarjeta.Name = "RadTarjeta"
        Me.RadTarjeta.Size = New System.Drawing.Size(81, 25)
        Me.RadTarjeta.TabIndex = 689
        Me.RadTarjeta.Text = "Tarjeta"
        Me.RadTarjeta.UseVisualStyleBackColor = True
        '
        'RadCheque
        '
        Me.RadCheque.AutoSize = True
        Me.RadCheque.Location = New System.Drawing.Point(1188, 80)
        Me.RadCheque.Name = "RadCheque"
        Me.RadCheque.Size = New System.Drawing.Size(86, 25)
        Me.RadCheque.TabIndex = 688
        Me.RadCheque.Text = "Cheque"
        Me.RadCheque.UseVisualStyleBackColor = True
        '
        'RadTransf
        '
        Me.RadTransf.AutoSize = True
        Me.RadTransf.Checked = True
        Me.RadTransf.Location = New System.Drawing.Point(1188, 38)
        Me.RadTransf.Name = "RadTransf"
        Me.RadTransf.Size = New System.Drawing.Size(130, 25)
        Me.RadTransf.TabIndex = 687
        Me.RadTransf.TabStop = True
        Me.RadTransf.Text = "Transferencia"
        Me.RadTransf.UseVisualStyleBackColor = True
        '
        'DTFechaF
        '
        Me.DTFechaF.AutoSize = False
        Me.DTFechaF.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic)
        Me.DTFechaF.Location = New System.Drawing.Point(947, 69)
        Me.DTFechaF.Mask = "00/00/0000"
        Me.DTFechaF.Name = "DTFechaF"
        Me.DTFechaF.Size = New System.Drawing.Size(207, 36)
        Me.DTFechaF.TabIndex = 686
        Me.DTFechaF.TabStop = False
        '
        'DTFechaI
        '
        Me.DTFechaI.AutoSize = False
        Me.DTFechaI.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTFechaI.Location = New System.Drawing.Point(714, 69)
        Me.DTFechaI.Mask = "00/00/0000"
        Me.DTFechaI.Name = "DTFechaI"
        Me.DTFechaI.Size = New System.Drawing.Size(207, 36)
        Me.DTFechaI.TabIndex = 674
        Me.DTFechaI.TabStop = False
        '
        'TxtCuenta
        '
        Me.TxtCuenta.AutoSize = False
        Me.TxtCuenta.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic)
        Me.TxtCuenta.Location = New System.Drawing.Point(289, 153)
        Me.TxtCuenta.Name = "TxtCuenta"
        Me.TxtCuenta.Size = New System.Drawing.Size(260, 36)
        Me.TxtCuenta.TabIndex = 675
        Me.TxtCuenta.ThemeName = "Material"
        '
        'TxtAlias
        '
        Me.TxtAlias.AutoSize = False
        Me.TxtAlias.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic)
        Me.TxtAlias.Location = New System.Drawing.Point(11, 152)
        Me.TxtAlias.Name = "TxtAlias"
        Me.TxtAlias.Size = New System.Drawing.Size(260, 36)
        Me.TxtAlias.TabIndex = 685
        Me.TxtAlias.ThemeName = "Material"
        '
        'LstCuentas
        '
        Me.LstCuentas.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstCuentas.Location = New System.Drawing.Point(567, 153)
        Me.LstCuentas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstCuentas.Name = "LstCuentas"
        Me.LstCuentas.SelectItem = ""
        Me.LstCuentas.SelectText = ""
        Me.LstCuentas.Size = New System.Drawing.Size(312, 36)
        Me.LstCuentas.TabIndex = 684
        '
        'lstBancos
        '
        Me.lstBancos.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBancos.Location = New System.Drawing.Point(373, 69)
        Me.lstBancos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstBancos.Name = "lstBancos"
        Me.lstBancos.SelectItem = ""
        Me.lstBancos.SelectText = ""
        Me.lstBancos.Size = New System.Drawing.Size(312, 36)
        Me.lstBancos.TabIndex = 683
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(11, 69)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(337, 36)
        Me.lstCliente.TabIndex = 682
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(564, 132)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(152, 18)
        Me.Label8.TabIndex = 681
        Me.Label8.Text = "Cuenta del Catalogo:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(292, 133)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(106, 18)
        Me.Label9.TabIndex = 680
        Me.Label9.Text = "No de Cuenta:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(944, 44)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 18)
        Me.Label10.TabIndex = 679
        Me.Label10.Text = "Fecha de Fin:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(711, 44)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(119, 18)
        Me.Label11.TabIndex = 678
        Me.Label11.Text = "Fecha de Inicio:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 18)
        Me.Label12.TabIndex = 677
        Me.Label12.Text = "Empresa:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(370, 44)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 18)
        Me.Label13.TabIndex = 676
        Me.Label13.Text = "Banco:"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.TablaBancos)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(3, 3)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1402, 323)
        Me.RadPanel1.TabIndex = 0
        '
        'TablaBancos
        '
        Me.TablaBancos.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TablaBancos.CmdActualizar_enabled = True
        Me.TablaBancos.Cmdcerrar_enabled = True
        Me.TablaBancos.CmdEliminar_enabled = True
        Me.TablaBancos.CmdExportaExcel_enabled = True
        Me.TablaBancos.Cmdguardar_enabled = True
        Me.TablaBancos.CmdNuevo_enabled = True
        Me.TablaBancos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaBancos.Location = New System.Drawing.Point(0, 0)
        Me.TablaBancos.Name = "TablaBancos"
        Me.TablaBancos.Size = New System.Drawing.Size(1402, 323)
        Me.TablaBancos.SqlSelect = "select"
        Me.TablaBancos.TabIndex = 0
        Me.TablaBancos.Tag = "P_Master"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TabPage2.Controls.Add(Me.Tabla_detalleBancos)
        Me.TabPage2.ImageIndex = 0
        Me.TabPage2.Location = New System.Drawing.Point(4, 39)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1408, 621)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Bancos del Sistema"
        '
        'Tabla_detalleBancos
        '
        Me.Tabla_detalleBancos.Cmdcerrar_enabled = True
        Me.Tabla_detalleBancos.CmdEditar_Enabled = True
        Me.Tabla_detalleBancos.CmdEliminar_enabled = True
        Me.Tabla_detalleBancos.CmdNuevo_Enabled = True
        Me.Tabla_detalleBancos.CmdRefrescar_enabled = True
        Me.Tabla_detalleBancos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla_detalleBancos.Location = New System.Drawing.Point(3, 3)
        Me.Tabla_detalleBancos.Name = "Tabla_detalleBancos"
        Me.Tabla_detalleBancos.Size = New System.Drawing.Size(1402, 615)
        Me.Tabla_detalleBancos.SqlSelect = ""
        Me.Tabla_detalleBancos.TabIndex = 0
        Me.Tabla_detalleBancos.Tag = "P_Parametros"
        '
        'Imagenes
        '
        Me.Imagenes.ImageStream = CType(resources.GetObject("Imagenes.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Imagenes.TransparentColor = System.Drawing.Color.Transparent
        Me.Imagenes.Images.SetKeyName(0, "BANCOSC.ico")
        '
        'Control_Bancos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1416, 664)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Control_Bancos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control de Bancos"
        Me.ThemeName = "MaterialBlueGrey"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.DTFechaF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTFechaI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCuenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAlias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents TablaBancos As Tabla_Filtro
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents RadTarjeta As RadioButton
    Friend WithEvents RadCheque As RadioButton
    Friend WithEvents RadTransf As RadioButton
    Friend WithEvents DTFechaF As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents DTFechaI As Telerik.WinControls.UI.RadMaskedEditBox
    Friend WithEvents TxtCuenta As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtAlias As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents LstCuentas As Listas
    Friend WithEvents lstBancos As Listas
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Tabla_detalleBancos As tabla_detalle
    Friend WithEvents Imagenes As ImageList
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
End Class

