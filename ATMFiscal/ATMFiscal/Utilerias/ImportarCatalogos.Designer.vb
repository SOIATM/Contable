<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportarCatalogos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportarCatalogos))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.CmdExportar = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImport = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.SegundoPlano = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        CType(Me.CmdExportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lstCliente)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.CmdExportar)
        Me.Panel1.Controls.Add(Me.CmdGuardar)
        Me.Panel1.Controls.Add(Me.CmdImport)
        Me.Panel1.Controls.Add(Me.CmdImportar)
        Me.Panel1.Controls.Add(Me.cmdCerrar)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1073, 82)
        Me.Panel1.TabIndex = 0
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(298, 29)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(450, 36)
        Me.lstCliente.TabIndex = 667
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(295, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 18)
        Me.Label20.TabIndex = 666
        Me.Label20.Text = "Empresa:"
        '
        'CmdExportar
        '
        Me.CmdExportar.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExportar.Location = New System.Drawing.Point(227, 11)
        Me.CmdExportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdExportar.Name = "CmdExportar"
        Me.CmdExportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdExportar.TabIndex = 665
        Me.CmdExportar.TabStop = False
        Me.CmdExportar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdExportar.ThemeName = "Aqua"
        '
        'CmdGuardar
        '
        Me.CmdGuardar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardar.Location = New System.Drawing.Point(173, 11)
        Me.CmdGuardar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardar.Name = "CmdGuardar"
        Me.CmdGuardar.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardar.TabIndex = 664
        Me.CmdGuardar.TabStop = False
        Me.CmdGuardar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdGuardar.ThemeName = "Aqua"
        '
        'CmdImport
        '
        Me.CmdImport.Image = Global.ATMFiscal.My.Resources.Resources.Importar_Datos
        Me.CmdImport.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImport.Location = New System.Drawing.Point(119, 11)
        Me.CmdImport.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImport.Name = "CmdImport"
        Me.CmdImport.Size = New System.Drawing.Size(50, 54)
        Me.CmdImport.TabIndex = 663
        Me.CmdImport.TabStop = False
        Me.CmdImport.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdImport.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(65, 11)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 662
        Me.CmdImportar.TabStop = False
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(11, 11)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 661
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 82)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowTemplate.Height = 24
        Me.Tabla.Size = New System.Drawing.Size(1073, 473)
        Me.Tabla.TabIndex = 9
        '
        'SegundoPlano
        '
        Me.SegundoPlano.WorkerSupportsCancellation = True
        '
        'ImportarCatalogos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1073, 555)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ImportarCatalogos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Importar de Catalogos"
        Me.ThemeName = "MaterialBlueGrey"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.CmdExportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents CmdExportar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdGuardar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdImport As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label20 As Label
    Friend WithEvents Tabla As DataGridView
    Friend WithEvents SegundoPlano As System.ComponentModel.BackgroundWorker
End Class

