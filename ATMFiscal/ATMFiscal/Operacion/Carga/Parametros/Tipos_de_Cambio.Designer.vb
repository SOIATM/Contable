<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Tipos_de_Cambio
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tipos_de_Cambio))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Tabla_detalleTiposCambio = New ATMFiscal.tabla_detalle()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tabla_detalleTiposCambio
        '
        Me.Tabla_detalleTiposCambio.Cmdcerrar_enabled = True
        Me.Tabla_detalleTiposCambio.CmdEditar_Enabled = True
        Me.Tabla_detalleTiposCambio.CmdEliminar_enabled = True
        Me.Tabla_detalleTiposCambio.CmdNuevo_Enabled = True
        Me.Tabla_detalleTiposCambio.CmdRefrescar_enabled = True
        Me.Tabla_detalleTiposCambio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla_detalleTiposCambio.Location = New System.Drawing.Point(0, 0)
        Me.Tabla_detalleTiposCambio.Margin = New System.Windows.Forms.Padding(2)
        Me.Tabla_detalleTiposCambio.Name = "Tabla_detalleTiposCambio"
        Me.Tabla_detalleTiposCambio.Size = New System.Drawing.Size(743, 649)
        Me.Tabla_detalleTiposCambio.SqlSelect = ""
        Me.Tabla_detalleTiposCambio.TabIndex = 0
        Me.Tabla_detalleTiposCambio.Tag = "P_Master"
        '
        'Tipos_de_Cambio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(743, 649)
        Me.ControlBox = False
        Me.Controls.Add(Me.Tabla_detalleTiposCambio)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Tipos_de_Cambio"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tipos de Cambio"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents Tabla_detalleTiposCambio As tabla_detalle
End Class

