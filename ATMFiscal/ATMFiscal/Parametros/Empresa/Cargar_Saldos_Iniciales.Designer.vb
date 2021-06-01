<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cargar_Saldos_Iniciales
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Cargar_Saldos_Iniciales))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.CmdPlantilla = New Telerik.WinControls.UI.RadButton()
        Me.Barra = New Telerik.WinControls.UI.RadProgressBar()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdPlantilla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.CmdPlantilla)
        Me.RadPanel1.Controls.Add(Me.Barra)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdImportar)
        Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(697, 94)
        Me.RadPanel1.TabIndex = 0
        '
        'CmdPlantilla
        '
        Me.CmdPlantilla.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdPlantilla.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPlantilla.Location = New System.Drawing.Point(218, 2)
        Me.CmdPlantilla.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdPlantilla.Name = "CmdPlantilla"
        Me.CmdPlantilla.Size = New System.Drawing.Size(50, 54)
        Me.CmdPlantilla.TabIndex = 603
        Me.CmdPlantilla.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdPlantilla.ThemeName = "Aqua"
        Me.Ayuda.SetToolTip(Me.CmdPlantilla, "Este boton permite generar una plantilla para cargar los saldos iniciales")
        '
        'Barra
        '
        Me.Barra.Location = New System.Drawing.Point(7, 61)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(261, 24)
        Me.Barra.TabIndex = 602
        Me.Barra.Text = " "
        Me.Barra.ThemeName = "Material"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(274, 24)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(411, 36)
        Me.lstCliente.TabIndex = 601
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(270, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 600
        Me.Label3.Text = "Empresa:"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(2, 2)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 600
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Importar_Datos
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(110, 2)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 598
        Me.CmdImportar.TabStop = False
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdImportar.ThemeName = "Aqua"
        Me.Ayuda.SetToolTip(Me.CmdImportar, "Boton para importar el archivo de datos")
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(164, 2)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 601
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        Me.Ayuda.SetToolTip(Me.Cmd_Procesar, "Importar información a la base de datos")
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(56, 2)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 599
        Me.CmdLimpiar.TabStop = False
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToDeleteRows = False
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 94)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.ReadOnly = True
        Me.Tabla.Size = New System.Drawing.Size(697, 290)
        Me.Tabla.TabIndex = 11
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'Cargar_Saldos_Iniciales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(697, 384)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Cargar_Saldos_Iniciales"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cargar Saldos Iniciales"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdPlantilla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Barra As Telerik.WinControls.UI.RadProgressBar
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents Tabla As DataGridView
    Friend WithEvents CmdPlantilla As Telerik.WinControls.UI.RadButton
    Friend WithEvents Ayuda As ToolTip
End Class

