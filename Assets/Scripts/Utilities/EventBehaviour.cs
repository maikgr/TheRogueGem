using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
namespace RogueGem.Utilities {
    class EventBehaviour : MonoBehaviour {
        private Dictionary<GameEvent, UnityEvent> eventMap;

        private static EventBehaviour eventController;

        public static EventBehaviour instance { get {
                if (!eventController) {
                    eventController = FindObjectOfType(typeof(EventBehaviour)) as EventBehaviour;
                    if (!eventController) {
                        Debug.LogError("Event Controller not found.");
                    } else {
                        eventController.Initialize();
                    }
                }
                return eventController;
            } }

        private void Initialize() {
            if(eventMap == null) {
                eventMap = new Dictionary<GameEvent, UnityEvent>();
            }
        }

        public static void StartListening(GameEvent eventType, UnityAction listener) {
            UnityEvent gameEvent = null;
            if(instance.eventMap.TryGetValue(eventType, out gameEvent)) {
                gameEvent.AddListener(listener);
            } else {
                gameEvent = new UnityEvent();
                gameEvent.AddListener(listener);
                instance.eventMap.Add(eventType, gameEvent);
            }
        }

        public static void StopListening(GameEvent eventType, UnityAction listener) {
            if (eventController == null)
                return;

            UnityEvent gameEvent = new UnityEvent();
            if (instance.eventMap.TryGetValue(eventType, out gameEvent)) {
                gameEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(GameEvent eventType) {
            UnityEvent gameEvent = new UnityEvent();
            if (instance.eventMap.TryGetValue(eventType, out gameEvent)) {
                gameEvent.Invoke();
            }
        }
    }

    public enum GameEvent {
        MoveEnemy,
        AttackMode
    }
}
