Public Class wbs_updatevt
    Private QLNET As String = My.Settings.QualiSrv


    Public MysqlRepository As New MySql_Repository
    Public LogWriter As New LogWriter

    ''' <summary>
    ''' FUNÇÃO ATUALIZA TABELAS WEBSERVICE VT (wbs_servicos, wbs_backlog)
    ''' </summary>
    ''' <returns>true/false</returns>
    ''' <remarks>ENJOY</remarks>
    Public Function WbsServicosupdate() As Boolean
        Dim REQUEST As Boolean = True

        Try
            Dim DS As New DataSet

            '// CFG URL
            Dim Address As String = QLNET
            Dim cid As String = "iCOD_OPERADORA=" & "923,363,330,332,075,684,710,770,794,278,996,091,686,693,691,078,692,694,531,193,758,720,884,695,083,689," & _
                                                    "088,086,389,641,430,437,584,603,087,687,685,089,333,752,093,690,688,700,890,294,560,757"
            Dim IdOS As String = "iID_TIPO_OS=22,26,38,27,21,61,10,67,48,49,50,62,42,204,69"

            '// RUN READ XML
            Dim xml As String = Address & cid & "&" & IdOS '// MOUNT URL
            DS.ReadXml(xml)

            Dim str As String
            'Dim CODOPERADORA As String = ""
            Dim DATETIME As String = Format(Now, "yyyy-MM-dd hh:mm:ss").ToString

            Dim DicCount As New Dictionary(Of String, Integer)

            '// SET DATA AS 1910-01-01 00:00:00
            MysqlRepository.MysqlUpdate("ora_wbs.wbs_backlog", "wbs_update = '1910-01-01 00:00:00'")

            '// EACH RESULT
            For Each TRows In DS.Tables(0).Rows

                '// IF DICTIONARY IS NULL, THEN CREATE
                If Not DicCount.ContainsKey(TRows("COD_OPERADORA").ToString()) Then DicCount(TRows("COD_OPERADORA").ToString()) = 0
                DicCount(TRows("COD_OPERADORA").ToString()) += 1


                '# DESCONFIO QUE DEVE HAVER CASOS DE ASPAS OU ASPAS DUPLAS...

                '// CREATE INSERT VALUE STRING
                str = "('" & _
                    TRows("CID_CONTRATO").ToString() & "', '" & _
                    TRows("COD_OPERADORA").ToString() & "', '" & _
                    TRows("COD_OS").ToString() & "', '" & _
                    TRows("NUM_CONTRATO").ToString() & "', '" & _
                    TRows("SEGMENTO_DESCR").ToString() & "', '" & _
                    TRows("OS_RESUMO").ToString() & "', '" & _
                    TRows("DT_CADASTRO").ToString() & "', '" & _
                    TRows("DT_AGENDA").ToString() & "', '" & _
                    TRows("AGENDA_DESCR").ToString() & "', '" & _
                    TRows("TEL_RES").ToString() & "', '" & _
                    TRows("TEL_CEL").ToString() & "', '" & _
                    TRows("TEL_COM").ToString() & "', '" & _
                    TRows("AREA_DESCRICAO").ToString() & "', '" & _
                    TRows("WO_ID").ToString() & "', '" & _
                    TRows("WO_STATUS").ToString() & "', '" & _
                    TRows("WO_STATUS_DESCRICAO").ToString() & "', '" & _
                    TRows("WO_JOBSTATUS").ToString() & "', '" & _
                    TRows("WO_SUBSTATUS").ToString() & "', '" & _
                    TRows("WO_STATUSDESC").ToString() & "', '" & _
                    TRows("WO_EQUIPE_LOGIN").ToString() & "', '" & _
                    TRows("WO_EQUIPE_TECNICA").ToString() & "', '" & _
                    TRows("WO_EQUIPE_CELULAR").ToString() & "', '" & _
                    TRows("FN_CONVENIENCIA").ToString() & "', '" & _
                    TRows("COD_HUB").ToString() & "', '" & _
                    TRows("ID_REGIAO").ToString() & "', '" & _
                    TRows("COD_NODE").ToString() & "', '" & _
                    TRows("COD_IMOVEL").ToString() & "', '" & _
                    TRows("END_COMPLETO").ToString() & "', '" & _
                    TRows("ID_COMPL1").ToString() & "', '" & _
                    TRows("COMPL1_DESCR").ToString() & "', '" & _
                    TRows("DT_MAX_ATEND_PRAZO").ToString() & "', '" & _
                    TRows("DT_UPDATE").ToString() & "', " & _
                    "'" & DATETIME & "')"

                '// ADD DATA
                MysqlRepository.MysqlAdd("IGNORE", "ora_wbs.wbs_servicos", str, "")
                MysqlRepository.MysqlAdd("", "ora_wbs.wbs_backlog", str, "ON DUPLICATE KEY UPDATE wbs_update ='" & DATETIME & "'")
            Next

            '// DEL OLD DATA
            MysqlRepository.MysqlDEL("ora_wbs.wbs_backlog", "wbs_update = '1910-01-01 00:00:00'")

            '// ADD BACKLOG IF 1h am
            If Now.Hour < 1 Then resumoBacklog(DicCount)

        Catch ex As Exception
            LogWriter.WhriteLog(Now, ex.Message, "WEB SRV UPDATE")
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