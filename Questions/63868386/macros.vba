Sub Test()
    Dim idColl, dupAdd, ws As Worksheet, adColl As Collection, dic As Object, r As Range, c As Range
    Application.ScreenUpdating = False
        Set dic = CreateObject("Scripting.Dictionary")
        For Each ws In ThisWorkbook.Sheets
            For Each r In ws.Range("A1:A" & ws.Cells(Rows.Count, 1).End(xlUp).Row)
                If Not IsEmpty(r) Then
                    If Not dic.Exists(r.Value) Then
                        Set dic(r.Value) = New Collection
                    End If
                    dic(r.Value).Add GetFullAddress(r)
                End If
            Next r
        Next ws
For Each idColl In dic
    Set adColl = dic(idColl)
    If adColl.Count >= 2 Then
        For i = 2 To adColl.Count
            dupAdd = adColl(i)
            Set c = Range(dupAdd)
            c.Interior.ColorIndex = 3
        Next
    End If
Next idColl
    Application.ScreenUpdating = True
End Sub

Private Function GetFullAddress(c As Range) As String
    GetFullAddress = "'" & c.Parent.Name & "'!" & c.Address(external:=False)
End Function
