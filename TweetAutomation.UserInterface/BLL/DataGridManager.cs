using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TweetAutomation.LoggingSystem.BusinessLogic;
using TweetAutomation.UserInterface.Model;

namespace TweetAutomation.UserInterface.BLL
{
  public class DataGridManager
  {
    private ILogRepository _logger = LogRepository.LogInstance();
    public DataGridManager() { }
    public void UpdateStatus(DataGridView dataGrid, Tweet record)
    {
      _logger.Update("DEBUG", $"Updating status on DataGrid. ID: {record.ID}");
      int rowCount = dataGrid.Rows.Count;
      for (int i = 0; i < rowCount - 1; i++)
      {
        if (dataGrid.Rows[i].Cells[0].Value.ToString() == record.ID.ToString())
        {
          dataGrid.Rows[i].Cells[4].Value = record.Status.ToString().Replace('_', ' ');
          return;
        }
      }
    }
  }
}
