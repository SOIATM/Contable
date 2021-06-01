<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Control_Tasas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Control_Tasas))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Tabla_Tasas = New ATMFiscal.tabla_detalle()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tabla_Tasas
        '
        Me.Tabla_Tasas.Cmdcerrar_enabled = True
        Me.Tabla_Tasas.CmdEditar_Enabled = True
        Me.Tabla_Tasas.CmdEliminar_enabled = True
        Me.Tabla_Tasas.CmdNuevo_Enabled = True
        Me.Tabla_Tasas.CmdRefrescar_enabled = True
        Me.Tabla_Tasas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla_Tasas.Location = New System.Drawing.Point(0, 0)
        Me.Tabla_Tasas.Margin = New System.Windows.Forms.Padding(2)
        Me.Tabla_Tasas.Name = "Tabla_Tasas"
        Me.Tabla_Tasas.Size = New System.Drawing.Size(894, 272)
        Me.Tabla_Tasas.SqlSelect = ""
        Me.Tabla_Tasas.TabIndex = 0
        Me.Tabla_Tasas.Tag = "P_Master"
        '
        'Control_Tasas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 272)
        Me.Controls.Add(Me.Tabla_Tasas)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Control_Tasas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Tasas"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Tabla_Tasas As tabla_detalle
End Class

