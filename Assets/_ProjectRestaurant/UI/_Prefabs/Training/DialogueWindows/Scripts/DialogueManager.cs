public class DialogueManager
{
    private EndDialogueUI _endDialogue;
    private StartDialogueUI _startDialogue;
    private TaskDialogueUI _taskDialogue;
    private MiniTaskDialogueUI _miniTaskDialogue;
    
    private GetTableTutorialDecorator _getTableApple;
    private GetTableTutorialDecorator _getTableOrange;
    private GiveTableTutorialDecorator _giveTable;
    private CuttingTableTutorialDecorator _cuttingTable;
    private DistributionTutorialDecorator _distribution;
    
    private FactoryUIGameplay _factoryUIGameplay;
    
    private void StartWedding()
    {
        _startDialogue.Show();
        _startDialogue.Button.onClick.AddListener(StartCookingSalat);
    }
    
    private void StartCookingSalat()
    {
        _startDialogue.Button.onClick.RemoveListener(StartCookingSalat);
        _taskDialogue.Show();
        _taskDialogue.Button.onClick.AddListener(StartTakeApple);
    }
    
    // выдали задание взять яблоко
    private void StartTakeApple()
    {
        _taskDialogue.Button.onClick.RemoveListener(StartTakeApple);
        _miniTaskDialogue.Show();
        _getTableApple.StartBlink();
        _miniTaskDialogue.Button.onClick.AddListener();// вкл заказы 
        _miniTaskDialogue.Button.onClick.AddListener();// вкл управление
    }
    
    //взял яблоко
    private void StartTellGarbage()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener();// отписка
        _miniTaskDialogue.Button.onClick.RemoveListener();// отписка
        
        _getTableApple.StopBlink();
        // выкл управление
        // меняем текст (рассказать про мусорку что можно выкинуть предмет)
        _miniTaskDialogue.Show();
        
        _miniTaskDialogue.Button.onClick.AddListener(StartBringCuttingTable);
    }
    
    // рассказали про мусорку
    private void StartBringCuttingTable() 
    {
        _miniTaskDialogue.Button.onClick.RemoveListener(StartBringCuttingTable);// отписка
        
        // меняем текст (задание положить предмет на разделочный стол)
        _miniTaskDialogue.Show();
        
        _miniTaskDialogue.Button.onClick.AddListener();// вкл управление
        _miniTaskDialogue.Button.onClick.AddListener(_cuttingTable.StartBlink);// вкл мигание
        
        //_miniTaskDialogue.ChangeText();
        //_miniTaskDialogue.Show();
        //_cuttingTable.StartBlink();
    }
    
    // положил яблоко на разделочный стол
    private void StartTakeOrange()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener();// отписка
        _miniTaskDialogue.Button.onClick.RemoveListener(_cuttingTable.StartBlink);// отписка

        _cuttingTable.StopBlink();
        // выкл управление
        // меняем текст (взять апельсин)
        _miniTaskDialogue.Show();

        _miniTaskDialogue.Button.onClick.AddListener();// вкл управление
        _miniTaskDialogue.Button.onClick.AddListener(_getTableOrange.StartBlink);// вкл мигание

        //_miniTaskDialogue.ChangeText();
        //_miniTaskDialogue.Show();
        //_getTableOrange.StartBlink();
    }

    // взял апельсин
    private void StartSecondBringCuttingTable()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener();// отписка
        _miniTaskDialogue.Button.onClick.RemoveListener(_getTableOrange.StartBlink);// отписка
        
        _cuttingTable.StopBlink();
        // выкл управление
        // меняем текст ()
        _miniTaskDialogue.Show();
        
        _miniTaskDialogue.Button.onClick.AddListener();// вкл управление
    }
    
    // салат был приготовлен
    private void StartCompliment()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener();// отписка
        
        // выкл управление
        // меняем текст ()
        _taskDialogue.Show();
        _taskDialogue.Button.onClick.AddListener(StartWaitOrder);
    }
    
    private void StartWaitOrder()
    {
        _taskDialogue.Button.onClick.RemoveListener(StartWaitOrder);// отписка
        
        // меняем текст ()
        _miniTaskDialogue.Show();
        
        _miniTaskDialogue.Button.onClick.AddListener();// вкл управление
        _miniTaskDialogue.Button.onClick.AddListener();// добавить чек
        _miniTaskDialogue.Button.onClick.AddListener(_distribution.StartBlink);// вкл мигание
    }
    
    // чек появился
    // заказ на Distribution
    // анимация закончена
   
    
    private void StartGoodbye()
    {
        _miniTaskDialogue.Button.onClick.RemoveListener();// вкл управление
        _miniTaskDialogue.Button.onClick.RemoveListener();// добавить чек
        _miniTaskDialogue.Button.onClick.RemoveListener(_distribution.StartBlink);// вкл мигание
        _endDialogue.Show();
    }

}
