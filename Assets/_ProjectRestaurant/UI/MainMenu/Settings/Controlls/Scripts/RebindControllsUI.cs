using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RebindControllsUI : MonoBehaviour, RebindControllsUI.IResetToDefault
{
    /// <summary>
    /// Ссылка на действие, которое нужно переназначить.
    /// </summary>
    public InputActionReference actionReference
    {
        get => m_Action;
        set
        {
            m_Action = value;
            UpdateActionLabel();
            UpdateBindingDisplay();
        }
    }

    /// <summary>
    /// ID (в строковой форме) привязки (binding), которую нужно переназначить в действии.
    /// </summary>
    /// <seealso cref="InputBinding.id"/>
    public string bindingId
    {
        get => m_BindingId;
        set
        {
            m_BindingId = value;
            UpdateBindingDisplay();
        }
    }

    /// <summary>
    /// Опции для форматирования отображаемой строки привязки.
    /// </summary>
    public InputBinding.DisplayStringOptions displayStringOptions
    {
        get => m_DisplayStringOptions;
        set
        {
            m_DisplayStringOptions = value;
            UpdateBindingDisplay();
        }
    }

    /// <summary>
    /// Текстовый компонент, который получает название действия. Опционально.
    /// </summary>
    public TextMeshProUGUI actionLabel
    {
        get => m_ActionLabel;
        set
        {
            m_ActionLabel = value;
            UpdateActionLabel();
        }
    }

    /// <summary>
    /// Текстовый компонент, который получает отображаемую строку привязки. Может быть <c>null</c>, в этом случае
    /// компонент полностью полагается на событие <see cref="updateBindingUIEvent"/>.
    /// </summary>
    public TextMeshProUGUI bindingText
    {
        get => m_BindingText;
        set
        {
            m_BindingText = value;
            UpdateBindingDisplay();
        }
    }

    /// <summary>
    /// Опциональный текстовый компонент, который получает текстовую подсказку в ожидании ввода от управления.
    /// </summary>
    /// <seealso cref="startRebindEvent"/>
    /// <seealso cref="rebindOverlay"/>
    public TextMeshProUGUI rebindPrompt
    {
        get => m_RebindText;
        set => m_RebindText = value;
    }

    /// <summary>
    /// Опциональный UI-объект, который активируется при запуске интерактивного переназначения и деактивируется по его завершении.
    /// Обычно используется для отображения наложения (overlay) поверх текущего UI, пока система ожидает ввода от управления.
    /// </summary>
    /// <remarks>
    /// Если не заданы ни <see cref="rebindPrompt"/>, ни <c>rebindOverlay</c>, компонент временно заменит
    /// текст в <see cref="bindingText"/> (если он не <c>null</c>) на <c>"Waiting..."</c>.
    /// </remarks>
    /// <seealso cref="startRebindEvent"/>
    /// <seealso cref="rebindPrompt"/>
    public GameObject rebindOverlay
    {
        get => m_RebindOverlay;
        set => m_RebindOverlay = value;
    }

    /// <summary>
    /// Событие, вызываемое каждый раз при обновлении UI для отражения текущей привязки.
    /// Может быть использовано для привязки пользовательской визуализации к привязкам.
    /// </summary>
    public UpdateBindingUIEvent updateBindingUIEvent
    {
        get
        {
            if (m_UpdateBindingUIEvent == null)
                m_UpdateBindingUIEvent = new UpdateBindingUIEvent();
            return m_UpdateBindingUIEvent;
        }
    }

    /// <summary>
    /// Событие, вызываемое при запуске интерактивного переназначения действия.
    /// </summary>
    public InteractiveRebindEvent startRebindEvent
    {
        get
        {
            if (m_RebindStartEvent == null)
                m_RebindStartEvent = new InteractiveRebindEvent();
            return m_RebindStartEvent;
        }
    }

    /// <summary>
    /// Событие, вызываемое при завершении или отмене интерактивного переназначения.
    /// </summary>
    public InteractiveRebindEvent stopRebindEvent
    {
        get
        {
            if (m_RebindStopEvent == null)
                m_RebindStopEvent = new InteractiveRebindEvent();
            return m_RebindStopEvent;
        }
    }

    /// <summary>
    /// Когда интерактивное переназначение выполняется, это контроллер операции переназначения.
    /// В противном случае равен <c>null</c>.
    /// </summary>
    public InputActionRebindingExtensions.RebindingOperation ongoingRebind => m_RebindOperation;

    /// <summary>
    /// Возвращает действие и индекс привязки, на которую нацелен компонент.
    /// </summary>
    /// <param name="action">Найденное действие.</param>
    /// <param name="bindingIndex">Найденный индекс привязки.</param>
    /// <returns>True, если действие и привязка были успешно найдены.</returns>
    public bool ResolveActionAndBinding(out InputAction action, out int bindingIndex)
    {
        bindingIndex = -1;

        action = m_Action?.action;
        if (action == null)
            return false;

        if (string.IsNullOrEmpty(m_BindingId))
            return false;

        // Look up binding index.
        var bindingId = new Guid(m_BindingId);
        bindingIndex = action.bindings.IndexOf(x => x.id == bindingId);
        if (bindingIndex == -1)
        {
            Debug.LogError($"Cannot find binding with ID '{bindingId}' on '{action}'", this);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Запускает обновление отображаемой в данный момент привязки.
    /// </summary>
    public void UpdateBindingDisplay()
    {
        var displayString = string.Empty;
        var deviceLayoutName = default(string);
        var controlPath = default(string);

        // Get display string from action.
        var action = m_Action?.action;
        if (action != null)
        {
            var bindingIndex = action.bindings.IndexOf(x => x.id.ToString() == m_BindingId);
            if (bindingIndex != -1)
                displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath, displayStringOptions);
        }

        // Set on label (if any).
        if (m_BindingText != null)
            m_BindingText.text = displayString;

        // Give listeners a chance to configure UI in response.
        m_UpdateBindingUIEvent?.Invoke(this, displayString, deviceLayoutName, controlPath);
    }

    /// <summary>
    /// Удаляет текущие примененные переопределения привязок (возвращает к значениям по умолчанию).
    /// </summary>
    public void ResetToDefault()
    {
        if (!ResolveActionAndBinding(out var action, out var bindingIndex))
            return;

        if (action.bindings[bindingIndex].isComposite)
        {
            // It's a composite. Remove overrides from part bindings.
            for (var i = bindingIndex + 1; i < action.bindings.Count && action.bindings[i].isPartOfComposite; ++i)
                action.RemoveBindingOverride(i);
        }
        else
        {
            action.RemoveBindingOverride(bindingIndex);
        }
        UpdateBindingDisplay();
        
        // Сохраняем изменения
        SaveBindings();
    }

    /// <summary>
    /// Инициирует интерактивное переназначение, которое позволяет игроку использовать контроль
    /// для выбора новой привязки для действия.
    /// </summary>
    public void StartInteractiveRebind()
    {
        if (!ResolveActionAndBinding(out var action, out var bindingIndex))
            return;

        // Если привязка композитная, нам нужно переназначить каждую часть по очереди.
        if (action.bindings[bindingIndex].isComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
                PerformInteractiveRebind(action, firstPartIndex, allCompositeParts: true);
        }
        else
        {
            PerformInteractiveRebind(action, bindingIndex);
        }
    }

    private void PerformInteractiveRebind(InputAction action, int bindingIndex, bool allCompositeParts = false)
{
    m_RebindOperation?.Cancel(); // Will null out m_RebindOperation.

    void CleanUp()
    {
        m_RebindOperation?.Dispose();
        m_RebindOperation = null;
        action.Enable();
    }

    //Fixes the "InvalidOperationException: Cannot rebind action x while it is enabled" error
    action.Disable();

    // Configure the rebind.
    m_RebindOperation = action.PerformInteractiveRebinding(bindingIndex)
        .OnCancel(
            operation =>
            {
                m_RebindStopEvent?.Invoke(this, operation);
                if (m_RebindOverlay != null)
                    m_RebindOverlay.SetActive(false);
                UpdateBindingDisplay();
                CleanUp();
            })
        .OnComplete(
            operation =>
            {
                var newBinding = action.bindings[bindingIndex];
                var newControlPath = newBinding.effectivePath;

                // Проверяем, не используется ли эта клавиша в других действиях
                if (!string.IsNullOrEmpty(newControlPath))
                {
                    RemoveBindingFromOtherActions(action, bindingIndex, newControlPath);
                }

                if (m_RebindOverlay != null)
                    m_RebindOverlay.SetActive(false);
                m_RebindStopEvent?.Invoke(this, operation);
                UpdateBindingDisplay();
                CleanUp();

                // Сохраняем изменения после успешного ребиндинга
                SaveBindings();

                // If there's more composite parts we should bind, initiate a rebind
                // for the next part.
                if (allCompositeParts)
                {
                    var nextBindingIndex = bindingIndex + 1;
                    if (nextBindingIndex < action.bindings.Count && action.bindings[nextBindingIndex].isPartOfComposite)
                        PerformInteractiveRebind(action, nextBindingIndex, true);
                }
            });

    // Отключаем отмену по Esc
    m_RebindOperation.WithCancelingThrough("<Keyboard>/escape");

    // If it's a part binding, show the name of the part in the UI.
    var partName = default(string);
    if (action.bindings[bindingIndex].isPartOfComposite)
        partName = $"Binding '{action.bindings[bindingIndex].name}'. ";

    // Bring up rebind overlay, if we have one.
    if (m_RebindOverlay != null)
        m_RebindOverlay.SetActive(true);
    
    if (m_RebindText != null)
    {
        var text = !string.IsNullOrEmpty(m_RebindOperation.expectedControlType)
            ? $"{partName}Waiting for {m_RebindOperation.expectedControlType} input..."
            : $"{partName}Waiting for input...";
        m_RebindText.text = text;
    }

    // If we have no rebind overlay and no callback but we have a binding text label,
    // temporarily set the binding text label to "<Waiting>".
    if (m_RebindOverlay == null && m_RebindText == null && m_RebindStartEvent == null && m_BindingText != null)
        m_BindingText.text = "<Waiting...>";

    // Give listeners a chance to act on the rebind starting.
    m_RebindStartEvent?.Invoke(this, m_RebindOperation);

    m_RebindOperation.Start();
}

    /// <summary>
    /// Удаляет привязку из других действий, если она уже используется
    /// </summary>
    /// <summary>
    /// Удаляет привязку из других действий, если она уже используется
    /// </summary>
    private void RemoveBindingFromOtherActions(InputAction currentAction, int currentBindingIndex, string newControlPath)
    {
        var actionMap = currentAction.actionMap;
        var asset = actionMap.asset;

        // Сначала проверяем все действия в том же композитном биндинге
        if (currentBindingIndex > 0 && currentAction.bindings[currentBindingIndex].isPartOfComposite)
        {
            // Находим начало композитного биндинга
            int compositeStartIndex = currentBindingIndex;
            while (compositeStartIndex > 0 && currentAction.bindings[compositeStartIndex - 1].isPartOfComposite)
            {
                compositeStartIndex--;
            }
            compositeStartIndex--; // Переходим к самому композитному биндингу

            // Проверяем все части этого композитного биндинга
            for (int i = compositeStartIndex + 1; i < currentAction.bindings.Count; i++)
            {
                var binding = currentAction.bindings[i];
                if (!binding.isPartOfComposite) break;

                // Пропускаем текущий биндинг
                if (i == currentBindingIndex) continue;

                // Если нашли дубликат в том же композитном биндинге - очищаем его
                if (binding.effectivePath == newControlPath)
                {
                    // ВАЖНО: Устанавливаем пустую строку вместо удаления override
                    currentAction.ApplyBindingOverride(i, "");
                    Debug.Log($"Cleared duplicate binding from same composite: {currentAction.name}, binding index {i}");
                    
                    // Немедленно обновляем UI для этого биндинга
                    UpdateSpecificBindingDisplay(currentAction, i);
                }
            }
        }

        // Затем проверяем все остальные действия во всех Action Maps
        foreach (var map in asset.actionMaps)
        {
            foreach (var action in map.actions)
            {
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    var binding = action.bindings[i];
                    
                    // Пропускаем текущий биндинг
                    if (action == currentAction && i == currentBindingIndex)
                        continue;

                    // Если нашли такую же привязку в другом действии - очищаем её
                    if (binding.effectivePath == newControlPath)
                    {
                        // ВАЖНО: Устанавливаем пустую строку вместо удаления override
                        action.ApplyBindingOverride(i, "");
                        Debug.Log($"Cleared duplicate binding from {action.name}, binding index {i}");
                        
                        // Немедленно обновляем UI для этого биндинга
                        UpdateSpecificBindingDisplay(action, i);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Обновляет отображение конкретного биндинга
    /// </summary>
    private void UpdateSpecificBindingDisplay(InputAction action, int bindingIndex)
    {
        // Ищем все RebindControllsUI, которые отображают этот биндинг
        if (s_RebindActionUIs == null) return;
        
        foreach (var rebindUI in s_RebindActionUIs)
        {
            if (rebindUI.m_Action?.action == action)
            {
                if (rebindUI.ResolveActionAndBinding(out var uiAction, out var uiBindingIndex))
                {
                    if (uiBindingIndex == bindingIndex)
                    {
                        rebindUI.UpdateBindingDisplay();
                    }
                }
            }
        }
    }

    protected void OnEnable()
    {
        if (s_RebindActionUIs == null)
            s_RebindActionUIs = new List<RebindControllsUI>();
        s_RebindActionUIs.Add(this);
        if (s_RebindActionUIs.Count == 1)
            InputSystem.onActionChange += OnActionChange;

        // Обновляем отображение при включении
        UpdateBindingDisplay();
    }

    protected void OnDisable()
    {
        m_RebindOperation?.Dispose();
        m_RebindOperation = null;

        s_RebindActionUIs.Remove(this);
        if (s_RebindActionUIs.Count == 0)
        {
            s_RebindActionUIs = null;
            InputSystem.onActionChange -= OnActionChange;
        }
    }

    // When the action system re-resolves bindings, we want to update our UI in response. While this will
    // also trigger from changes we made ourselves, it ensures that we react to changes made elsewhere. If
    // the user changes keyboard layout, for example, we will get a BoundControlsChanged notification and
    // will update our UI to reflect the current keyboard layout.
    private static void OnActionChange(object obj, InputActionChange change)
    {
        if (change != InputActionChange.BoundControlsChanged)
            return;

        var action = obj as InputAction;
        var actionMap = action?.actionMap ?? obj as InputActionMap;
        var actionAsset = actionMap?.asset ?? obj as InputActionAsset;

        for (var i = 0; i < s_RebindActionUIs.Count; ++i)
        {
            var component = s_RebindActionUIs[i];
            var referencedAction = component.actionReference?.action;
            if (referencedAction == null)
                continue;

            if (referencedAction == action ||
                referencedAction.actionMap == actionMap ||
                referencedAction.actionMap?.asset == actionAsset)
                component.UpdateBindingDisplay();
        }
    }

    private void SaveBindings()
    {
        //var rebinds = m_Action.action.actionMap.asset.SaveBindingOverridesAsJson();
        //PlayerPrefs.SetString("rebinds", rebinds);
        //PlayerPrefs.Save();
        
        m_Action.action.actionMap.asset.SaveBindingOverridesAsJson();
    }

    [Tooltip("Reference to action that is to be rebound from the UI.")]
    [SerializeField]
    private InputActionReference m_Action;

    [SerializeField]
    private string m_BindingId;

    [SerializeField]
    private InputBinding.DisplayStringOptions m_DisplayStringOptions;

    [Tooltip("Text label that will receive the name of the action. Optional. Set to None to have the "
        + "rebind UI not show a label for the action.")]
    [SerializeField]
    private TextMeshProUGUI m_ActionLabel;

    [Tooltip("Текстовая метка, которая будет получать текущую отформатированную строку привязки.")]
    [SerializeField]
    private TextMeshProUGUI m_BindingText;

    [Tooltip("Optional UI that will be shown while a rebind is in progress.")]
    [SerializeField]
    private GameObject m_RebindOverlay;

    [Tooltip("Optional text label that will be updated with prompt for user input.")]
    [SerializeField]
    private TextMeshProUGUI m_RebindText;

    [Tooltip("Event that is triggered when the way the binding is display should be updated. This allows displaying "
        + "bindings in custom ways, e.g. using images instead of text.")]
    [SerializeField]
    private UpdateBindingUIEvent m_UpdateBindingUIEvent;

    [Tooltip("Event that is triggered when an interactive rebind is being initiated. This can be used, for example, "
        + "to implement custom UI behavior while a rebind is in progress. It can also be used to further "
        + "customize the rebind.")]
    [SerializeField]
    private InteractiveRebindEvent m_RebindStartEvent;

    [Tooltip("Event that is triggered when an interactive rebind is complete or has been aborted.")]
    [SerializeField]
    private InteractiveRebindEvent m_RebindStopEvent;

    private InputActionRebindingExtensions.RebindingOperation m_RebindOperation;

    private static List<RebindControllsUI> s_RebindActionUIs;

    // We want the label for the action name to update in edit mode, too, so
    // we kick that off from here.
    #if UNITY_EDITOR
    protected void OnValidate()
    {
        UpdateActionLabel();
        UpdateBindingDisplay();
    }

    #endif

    private void UpdateActionLabel()
    {
        if (m_ActionLabel != null)
        {
            var action = m_Action?.action;
            m_ActionLabel.text = action != null ? action.name : string.Empty;
        }
    }

    [Serializable]
    public class UpdateBindingUIEvent : UnityEvent<RebindControllsUI, string, string, string>
    {
    }

    [Serializable]
    public class InteractiveRebindEvent : UnityEvent<RebindControllsUI, InputActionRebindingExtensions.RebindingOperation>
    {
    }
    
    public interface IResetToDefault
    {
        public void ResetToDefault();
    }
}