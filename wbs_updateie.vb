Public Class wbs_updateie
    Private QLNET As String = My.Settings.QualiIE


    Public MysqlRepository As New MySql_Repository
    Public LogWriter As New LogWriter

    ''' <summary>
    ''' FUNÇÃO ATUALIZA TABELAS WEBSERVICE IE (wbs_servicos, wbs_backlog)
    ''' </summary>
    ''' <returns>true/false</returns>
    ''' <remarks>ENJOY</remarks>
    Public Function WbsIEupdate() As Boolean
        Dim REQUEST As Boolean = True


        Try
            Dim DS As New DataSet

            '// CFG URL
            Dim Address As String = QLNET
            Dim cid As String = "iCOD_OPERADORA=" & "923,363,330,332,075,684,710,770,794,278,996,091,686,693,691,078,692,694,531,193,758,720,884,695,083,689," & _
                                                    "088,086,389,641,430,437,584,603,087,687,685,089,333,752,093,690,688,700,890,294,560,757"

            '// RUN READ XML
            Dim xml As String = Address & cid '// MOUNT URL
            DS.ReadXml(xml)

            Dim str As String
            Dim DATETIME As String = Format(Now, "yyyy-MM-dd HH:mm:ss").ToString

            Dim DicCount As New Dictionary(Of String, Integer)

            MysqlRepository.MysqlUpdate("ora_wbs.wbs_ie_backlog", "wbs_update = '1999-01-01 00:00:00'")

            '// EACH RESULT
            For Each TRows In DS.Tables(0).Rows

                '// IF DICTIONARY IS NULL, THEN CREATE
                If Not DicCount.ContainsKey(TRows("COD_OPERADORA").ToString()) Then DicCount(TRows("COD_OPERADORA").ToString()) = 0
                DicCount(TRows("COD_OPERADORA").ToString()) += 1

                '// CREATE INSERT VALUE STRING
                str = "(" & _
                    TRows("CID_CONTRATO").ToString() & "," & _
                    TRows("COD_OPERADORA").ToString() & "," & _
                    TRows("ID_OCORRENCIA").ToString() & "," & _
                    TRows("NUM_CONTRATO").ToString() & ",'" & _
                    TRows("SEGMENTO_DESCR").ToString() & "','" & _
                    TRows("OCORRENCIA_DESCRICAO").ToString() & "','" & _
                    TRows("DT_OCORRENCIA").ToString() & "'," & _
                    IIf(TRows("TEL_RES") = "", 0, TRows("TEL_RES").Replace(" ", "").ToString()) & "," & _
                    IIf(TRows("TEL_CEL") = "", 0, TRows("TEL_CEL").Replace(" ", "").ToString()) & "," & _
                    IIf(TRows("TEL_COM") = "", 0, TRows("TEL_COM").Replace(" ", "").ToString()) & ",'" & _
                    TRows("COD_HUB").ToString() & "','" & _
                    TRows("ID_REGIAO").ToString() & "','" & _
                    TRows("COD_NODE").ToString() & "'," & _
                    TRows("COD_IMOVEL").ToString() & ",'" & _
                    TRows("END_COMPLETO").Replace("/", "-").ToString() & "','" & _
                    TRows("ID_COMPL1").ToString() & "','" & _
                    TRows("COMPL1_DESCR").ToString() & "'," & _
                    "'" & DATETIME & "')"

                '// ADD DATA
                MysqlRepository.MysqlAdd("IGNORE", "ora_wbs.wbs_ie", str, "")
                MysqlRepository.MysqlAdd("", "ora_wbs.wbs_ie_backlog", str, "ON DUPLICATE KEY UPDATE wbs_update = '" & DATETIME & "'")

            Next

            MysqlRepository.MysqlDEL("ora_wbs.wbs_ie_backlog", "wbs_update = '1999-01-01 00:00:00'")

            '// ADD BACKLOG IF 1h am
            If Now.Hour < 1 Then resumoBacklog(DicCount)

        Catch ex As Exception
            LogWriter.WhriteLog(Now, ex.Message, "WEB IE UPDATE")
        End Try

        Return REQUEST
    End Function

    ''' <summary>
    ''' BACKLOG VTs DO DIA
    ''' </summary>
    ''' <param name="Resumo">DADOS DE CADA DIA/CIDADE</param>
    ''' <remarks>ENJOY</remarks>
    Sub resumoBacklog(ByVal Resumo As Object)
        Dim str As String = ""

        For Each Res In Resumo
            str = "('" & Format(CDate(Now), "yyyy-MM-dd") & "','" & Res.key & "'," & Res.Value & ")"
            MysqlRepository.MysqlAdd("", "ora_wbs.wbs_backlog_resumo", str, "ON DUPLICATE KEY UPDATE COUNT_BACKLOG = " & Res.Value)
        Next
    End Sub

End Class