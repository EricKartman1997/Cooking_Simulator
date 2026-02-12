using Zenject;

public class DialogueManager
{
    private FactoryUIGameplay _factoryUIGameplay;
    private readonly IInputBlocker _inputBlocker;
    private ChecksManager _checksManager;
    private FactoryUITraining _factoryUITraining;
    private FactoryEnvironmentTraining _factoryEnvironmentTraining;

    private GetTableTutorialDecorator GetTableApple => _factoryEnvironmentTraining.GetTableApple;

    private GetTableTutorialDecorator GetTableOrange => _factoryEnvironmentTraining.GetTableOrange;

    private GiveTableTutorialDecorator GiveTable => _factoryEnvironmentTraining.GiveTable;

    private CuttingTableTutorialDecorator CuttingTable => _factoryEnvironmentTraining.CuttingTable;

    private DistributionTutorialDecorator Distribution => _factoryEnvironmentTraining.Distribution;

    private EndDialogueUI EndDialogue => _factoryUITraining.EndDialogue;

    private StartDialogueUI StartDialogue => _factoryUITraining.StartDialogue;

    private TaskDialogueUI TaskDialogue => _factoryUITraining.TaskDialogue;

    private MiniTaskDialogueUI MiniTaskDialogue => _factoryUITraining.MiniTaskDialogue;


    [Inject]
    public DialogueManager(IInputBlocker inputBlocker,ChecksManager checksManager,FactoryUITraining factoryUITraining, FactoryEnvironmentTraining factoryEnvironmentTraining)
    {
        _inputBlocker = inputBlocker;
        _checksManager = checksManager;
        _factoryUITraining = factoryUITraining;
        _factoryEnvironmentTraining = factoryEnvironmentTraining;
    }
    
    public void StartWedding() //вызывается из BootstrapTraining
    {
        DisableMovement();
        StartDialogue.Show();
        StartDialogue.Button.onClick.AddListener(StartCookingSalat);
    }
    
    private void StartCookingSalat() //вызывается из this
    {
        StartDialogue.Button.onClick.RemoveListener(StartCookingSalat);
        TaskDialogue.Show();
        TaskDialogue.Button.onClick.AddListener(StartTakeApple);
    }
    
    // выдали задание взять яблоко
    private void StartTakeApple() //вызывается из this
    {
        TaskDialogue.Button.onClick.RemoveListener(StartTakeApple);
        MiniTaskDialogue.Show();
        GetTableApple.StartBlink();
        
        GetTableApple.TookAppleAction += StartTellGarbage; //вызывается
        MiniTaskDialogue.Button.onClick.AddListener(_factoryUIGameplay.ShowOrder);// вкл заказы 
        MiniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
        
    }
    
    //взял яблоко
    private void StartTellGarbage() //вызывается из GetTableApple
    {
        GetTableApple.TookAppleAction -= StartTellGarbage;// отписываюсь
        MiniTaskDialogue.Button.onClick.RemoveListener(_factoryUIGameplay.ShowOrder);// отписка
        MiniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        
        GetTableApple.StopBlink();
        DisableMovement(); // выкл управление
        MiniTaskDialogue.ChangeText(DialogueText.GARBAGE_TITLE,DialogueText.GARBAGE_DESCRIPTION);// меняем текст (рассказать про мусорку что можно выкинуть предмет)
        MiniTaskDialogue.Show();
        
        MiniTaskDialogue.Button.onClick.AddListener(StartBringCuttingTable);
    }
    
    // рассказали про мусорку
    private void StartBringCuttingTable() //вызывается из this
    {
        MiniTaskDialogue.Button.onClick.RemoveListener(StartBringCuttingTable);// отписка
        
        MiniTaskDialogue.ChangeText(DialogueText.CUTTING_TABLE_TITLE,DialogueText.CUTTING_TABLE_DESCRIPTION);// меняем текст (задание положить предмет на разделочный стол)
        MiniTaskDialogue.Show();
        
        CuttingTable.PutAppleAction += StartTakeOrange; //вызывается
        MiniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
        MiniTaskDialogue.Button.onClick.AddListener(CuttingTable.StartBlink);// вкл мигание
        
    }
    
    // положил яблоко на разделочный стол
    private void StartTakeOrange() //вызывается из CuttingTable
    {
        CuttingTable.PutAppleAction -= StartTakeOrange;// отписываюсь
        MiniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        MiniTaskDialogue.Button.onClick.RemoveListener(CuttingTable.StartBlink);// отписка

        CuttingTable.StopBlink();
        DisableMovement(); // выкл управление
        MiniTaskDialogue.ChangeText(DialogueText.TAKE_ORANGE_TITLE,DialogueText.TAKE_ORANGE_DESCRIPTION);// меняем текст (взять апельсин)
        MiniTaskDialogue.Show();

        GetTableOrange.TookOrangeAction += StartSecondBringCuttingTable; //вызывается
        MiniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
        MiniTaskDialogue.Button.onClick.AddListener(GetTableOrange.StartBlink);// вкл мигание
    }

    // взял апельсин
    private void StartSecondBringCuttingTable() //вызывается из GetTableOrange
    {
        GetTableOrange.TookOrangeAction -= StartSecondBringCuttingTable;// отписываюсь
        MiniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        MiniTaskDialogue.Button.onClick.RemoveListener(GetTableOrange.StartBlink);// отписка
        
        CuttingTable.StopBlink();
        DisableMovement(); // выкл управление
        MiniTaskDialogue.ChangeText(DialogueText.SECOND_CUTTING_TABLE_TITLE,DialogueText.SECOND_CUTTING_TABLE_DESCRIPTION);// меняем текст ()
        MiniTaskDialogue.Show();

        CuttingTable.CookedSalatAction += StartCompliment; //вызывается
        MiniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
    }
    
    // салат был приготовлен
    private void StartCompliment() //вызывается из CuttingTable когда приготовился салат
    {
        CuttingTable.CookedSalatAction -= StartCompliment;// отписываюсь
        MiniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        
        DisableMovement(); // выкл управление
        TaskDialogue.ChangeText(DialogueText.COMPLIMENT_TITLE,DialogueText.COMPLIMENT_DESCRIPTION); // меняем текст ()
        TaskDialogue.Show();
        TaskDialogue.Button.onClick.AddListener(StartGiveTable);
    }
    
    private void StartGiveTable() //вызывается из this
    {
        TaskDialogue.Button.onClick.RemoveListener(StartGiveTable); // отписка
        
        MiniTaskDialogue.ChangeText(DialogueText.GIVE_TABLE_TITLE,DialogueText.GIVE_TABLE_DESCRIPTION);// меняем текст
        MiniTaskDialogue.Show();
        GiveTable.StartBlink();
        
        GiveTable.PutSalatAction += StartWaitOrder; //вызывается
        MiniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
    }
    
    // положил салат на GiveTable
    private void StartWaitOrder() //вызывается из GiveTable
    {
        GiveTable.PutSalatAction -= StartWaitOrder;// отписка
        MiniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        
        DisableMovement(); // выкл управление
        GiveTable.StopBlink();
        MiniTaskDialogue.ChangeText(DialogueText.WAIT_ORDER_TITLE,DialogueText.WAIT_ORDER_DESCRIPTION);// меняем текст ()
        MiniTaskDialogue.Show();
        
        Distribution.WasReadyOrderAction += StartGoodbye; //вызывается
        MiniTaskDialogue.Button.onClick.AddListener(EnableMovement);// вкл управление
        MiniTaskDialogue.Button.onClick.AddListener(_checksManager.AddCheckTutorial);// добавить чек
        MiniTaskDialogue.Button.onClick.AddListener(Distribution.StartBlink);// вкл мигание
    }
    
    // чек появился
    // заказ на Distribution
    // анимация закончена
    
    private void StartGoodbye() //вызывается из Distribution когда заказ принят
    {
        Distribution.WasReadyOrderAction -= StartGoodbye; // отписка
        MiniTaskDialogue.Button.onClick.RemoveListener(EnableMovement);// отписка
        MiniTaskDialogue.Button.onClick.RemoveListener(_checksManager.AddCheckTutorial);// отписка
        MiniTaskDialogue.Button.onClick.RemoveListener(Distribution.StartBlink);// отписка
        
        Distribution.StopBlink();
        EndDialogue.Show();
    }
    
    private void DisableMovement()
    {
        _inputBlocker.Block(this, InputBlockType.Movement);
    }

    private void EnableMovement()
    {
        _inputBlocker.Unblock(this, InputBlockType.Movement);
    }


}
