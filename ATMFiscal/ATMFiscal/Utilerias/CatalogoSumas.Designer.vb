<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CatalogoSumas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CatalogoSumas))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.CmdImpuesN = New Telerik.WinControls.UI.RadButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LstAnio = New System.Windows.Forms.ComboBox()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Barra = New Telerik.WinControls.UI.RadProgressBar()
        Me.CmdCosto = New Telerik.WinControls.UI.RadButton()
        Me.CmdImpuestos = New Telerik.WinControls.UI.RadButton()
        Me.CmdSumas = New Telerik.WinControls.UI.RadButton()
        Me.CmdCatalogo = New Telerik.WinControls.UI.RadButton()
        Me.CmdInv = New Telerik.WinControls.UI.RadButton()
        Me.CmdAnual = New Telerik.WinControls.UI.RadButton()
        Me.CmdTablas = New Telerik.WinControls.UI.RadButton()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.Tabla = New System.Windows.Forms.DataGridView()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdImpuesN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCosto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImpuestos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSumas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCatalogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdInv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdAnual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdTablas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.CmdImpuesN)
        Me.RadPanel1.Controls.Add(Me.Label4)
        Me.RadPanel1.Controls.Add(Me.LstAnio)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.Barra)
        Me.RadPanel1.Controls.Add(Me.CmdCosto)
        Me.RadPanel1.Controls.Add(Me.CmdImpuestos)
        Me.RadPanel1.Controls.Add(Me.CmdSumas)
        Me.RadPanel1.Controls.Add(Me.CmdCatalogo)
        Me.RadPanel1.Controls.Add(Me.CmdInv)
        Me.RadPanel1.Controls.Add(Me.CmdAnual)
        Me.RadPanel1.Controls.Add(Me.CmdTablas)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(958, 100)
        Me.RadPanel1.TabIndex = 0
        '
        'CmdImpuesN
        '
        Me.CmdImpuesN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImpuesN.Location = New System.Drawing.Point(494, 2)
        Me.CmdImpuesN.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImpuesN.Name = "CmdImpuesN"
        Me.CmdImpuesN.Size = New System.Drawing.Size(50, 54)
        Me.CmdImpuesN.TabIndex = 547
        Me.CmdImpuesN.TabStop = False
        Me.CmdImpuesN.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdImpuesN.ThemeName = "Aqua"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(939, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 18)
        Me.Label4.TabIndex = 552
        Me.Label4.Text = "Año:"
        '
        'LstAnio
        '
        Me.LstAnio.FormattingEnabled = True
        Me.LstAnio.Location = New System.Drawing.Point(942, 29)
        Me.LstAnio.Name = "LstAnio"
        Me.LstAnio.Size = New System.Drawing.Size(97, 24)
        Me.LstAnio.TabIndex = 553
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(547, 27)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 551
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(554, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 550
        Me.Label3.Text = "Empresa:"
        '
        'Barra
        '
        Me.Barra.Location = New System.Drawing.Point(8, 61)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(412, 29)
        Me.Barra.TabIndex = 549
        Me.Barra.Text = " "
        Me.Barra.ThemeName = "Material"
        '
        'CmdCosto
        '
        Me.CmdCosto.Image = Global.ATMFiscal.My.Resources.Resources.MONEDAS
        Me.CmdCosto.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCosto.Location = New System.Drawing.Point(55, 2)
        Me.CmdCosto.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdCosto.Name = "CmdCosto"
        Me.CmdCosto.Size = New System.Drawing.Size(50, 54)
        Me.CmdCosto.TabIndex = 548
        Me.CmdCosto.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdCosto.ThemeName = "Aqua"
        '
        'CmdImpuestos
        '
        Me.CmdImpuestos.Image = Global.ATMFiscal.My.Resources.Resources.Mensual
        Me.CmdImpuestos.ImageAlignment = System.Drawing.ContentAlignment.TopCenter
        Me.CmdImpuestos.Location = New System.Drawing.Point(268, 2)
        Me.CmdImpuestos.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImpuestos.Name = "CmdImpuestos"
        Me.CmdImpuestos.Size = New System.Drawing.Size(116, 54)
        Me.CmdImpuestos.TabIndex = 548
        Me.CmdImpuestos.Text = "Impuestos"
        Me.CmdImpuestos.TextAlignment = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdImpuestos.ThemeName = "Aqua"
        '
        'CmdSumas
        '
        Me.CmdSumas.Image = Global.ATMFiscal.My.Resources.Resources.Calculadora
        Me.CmdSumas.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSumas.Location = New System.Drawing.Point(2, 2)
        Me.CmdSumas.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSumas.Name = "CmdSumas"
        Me.CmdSumas.Size = New System.Drawing.Size(50, 54)
        Me.CmdSumas.TabIndex = 545
        Me.CmdSumas.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSumas.ThemeName = "Aqua"
        '
        'CmdCatalogo
        '
        Me.CmdCatalogo.Image = Global.ATMFiscal.My.Resources.Resources.CatalogoCuentas
        Me.CmdCatalogo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCatalogo.Location = New System.Drawing.Point(108, 2)
        Me.CmdCatalogo.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdCatalogo.Name = "CmdCatalogo"
        Me.CmdCatalogo.Size = New System.Drawing.Size(50, 54)
        Me.CmdCatalogo.TabIndex = 546
        Me.CmdCatalogo.TabStop = False
        Me.CmdCatalogo.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdCatalogo.ThemeName = "Aqua"
        '
        'CmdInv
        '
        Me.CmdInv.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdInv.Location = New System.Drawing.Point(215, 2)
        Me.CmdInv.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdInv.Name = "CmdInv"
        Me.CmdInv.Size = New System.Drawing.Size(50, 54)
        Me.CmdInv.TabIndex = 545
        Me.CmdInv.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdInv.ThemeName = "Aqua"
        '
        'CmdAnual
        '
        Me.CmdAnual.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdAnual.Location = New System.Drawing.Point(161, 2)
        Me.CmdAnual.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdAnual.Name = "CmdAnual"
        Me.CmdAnual.Size = New System.Drawing.Size(50, 54)
        Me.CmdAnual.TabIndex = 547
        Me.CmdAnual.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdAnual.ThemeName = "Aqua"
        '
        'CmdTablas
        '
        Me.CmdTablas.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdTablas.Location = New System.Drawing.Point(440, 2)
        Me.CmdTablas.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdTablas.Name = "CmdTablas"
        Me.CmdTablas.Size = New System.Drawing.Size(50, 54)
        Me.CmdTablas.TabIndex = 546
        Me.CmdTablas.TabStop = False
        Me.CmdTablas.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdTablas.ThemeName = "Aqua"
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToOrderColumns = True
        Me.Tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 100)
        Me.Tabla.Margin = New System.Windows.Forms.Padding(2)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowTemplate.Height = 24
        Me.Tabla.Size = New System.Drawing.Size(958, 244)
        Me.Tabla.TabIndex = 10
        '
        'CatalogoSumas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(958, 344)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CatalogoSumas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parámetros de Calculo Anual"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdImpuesN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCosto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImpuestos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSumas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCatalogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdInv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdAnual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdTablas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents CmdCosto As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImpuestos As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdSumas As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdCatalogo As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdInv As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdAnual As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdTablas As Telerik.WinControls.UI.RadButton
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Barra As Telerik.WinControls.UI.RadProgressBar
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents Ayuda As ToolTip
	Friend WithEvents Label4 As Label
	Friend WithEvents LstAnio As ComboBox
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents Tabla As DataGridView
	Friend WithEvents CmdImpuesN As Telerik.WinControls.UI.RadButton
End Class

