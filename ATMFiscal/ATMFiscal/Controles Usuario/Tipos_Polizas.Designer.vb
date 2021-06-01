<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tipos_Polizas
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblselect = New System.Windows.Forms.Label()
        Me.tabla = New System.Windows.Forms.DataGridView()
        CType(Me.tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblselect
        '
        Me.lblselect.AutoSize = True
        Me.lblselect.Location = New System.Drawing.Point(252, 24)
        Me.lblselect.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblselect.Name = "lblselect"
        Me.lblselect.Size = New System.Drawing.Size(47, 17)
        Me.lblselect.TabIndex = 8
        Me.lblselect.Text = "Select"
        Me.lblselect.Visible = False
        '
        'tabla
        '
        Me.tabla.AllowUserToAddRows = False
        Me.tabla.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.tabla.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.tabla.Dock = System.Windows.Forms.DockStyle.Top
        Me.tabla.Location = New System.Drawing.Point(0, 0)
        Me.tabla.Margin = New System.Windows.Forms.Padding(4)
        Me.tabla.Name = "tabla"
        Me.tabla.ReadOnly = True
        Me.tabla.Size = New System.Drawing.Size(321, 155)
        Me.tabla.TabIndex = 7
        '
        'Tipos_Polizas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblselect)
        Me.Controls.Add(Me.tabla)
        Me.Name = "Tipos_Polizas"
        Me.Size = New System.Drawing.Size(321, 155)
        CType(Me.tabla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblselect As Label
    Friend WithEvents tabla As DataGridView
End Class
