#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace JWOAGameSystem
{
    public class FloatText : MonoBehaviour
    {
        #region 单例
        private static FloatText m_Instance;
        private static FloatText Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    GameObject gameObject = new GameObject("FloatText");
                    // DontDestroyOnLoad(gameObject);
                    m_Instance = gameObject.AddComponent<FloatText>();
                }
                return m_Instance;
            }
        }
        #endregion
        #region Canvas设置
        //屏幕适配比例，如果不对记得改。
        //一般竖屏是1080*1920，横屏是1920*1080。
        private static Vector2 m_ScreenSize = new Vector2(1920, 1080);
        //不建议改。
        private static RenderMode m_RenderMode = RenderMode.ScreenSpaceOverlay;
        //同样不建议改
        private static CanvasScaler.ScaleMode m_ScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        //sort order,影响多个canvas的渲染顺序，根据需求更改，这里设置999，在最上层渲染。
        private static int m_SortOrder = 999;
        #endregion
        private Camera m_WorldTextLookAtCamera;
        public static Camera Camera { get => Instance.m_WorldTextLookAtCamera; set => Instance.m_WorldTextLookAtCamera = value; }
        private const string m_FontAssetAddres = "鸿蒙字体";//这个地方根据自己的项目改

        private Transform m_WorldTextRoot;
        private Transform m_UITextRoot;

        private GameObject m_TextTemplate_UI;//模板
        private GameObject m_TextTemplate_World;//模板
        private ObjectPool<TextMeshProUGUI> m_UITextPool;//对象池
        private ObjectPool<TextMeshPro> m_WorldTextPool;//对象池
        private List<TextMeshProUGUI> m_AllInstance_UIFloatText; //存储所有生成的实例
        private List<TextMeshPro> m_AllInstance_WorldFloatText;////存储所有生成的实例

        private TMP_FontAsset m_FontAsset;//字体资源
        private AsyncOperationHandle m_InitializeHandle;//通过isDone来确定是否已经完成初始化。
        private bool m_Initialized = false;//是否已经执行过一次初始化方法，用来防止二次被执行。

        #region 创建对象池需要的方法
        private TextMeshProUGUI Creat_UIText()
        {
            GameObject gameObject = Instantiate(m_TextTemplate_UI, m_UITextRoot, false);
            TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
            m_AllInstance_UIFloatText.Add(text);
            return gameObject.GetComponent<TextMeshProUGUI>();
        }
        private TextMeshPro Creat_WorldText()
        {
            GameObject gameObject = Instantiate(m_TextTemplate_World, m_WorldTextRoot, false);
            TextMeshPro text = gameObject.GetComponent<TextMeshPro>();
            m_AllInstance_WorldFloatText.Add(text);
            return gameObject.GetComponent<TextMeshPro>();
        }
        private void Get_UIText(TextMeshProUGUI text)
        {
            text.gameObject.SetActive(true);
        }
        private void Get_WorldText(TextMeshPro text)
        {
            text.gameObject.SetActive(true);
        }
        private void Release_UIText(TextMeshProUGUI text)
        {
            text.gameObject.SetActive(false);
        }
        private void Release_WorldText(TextMeshPro text)
        {
            text.gameObject.SetActive(false);
        }
        private void Destroy_UIText(TextMeshProUGUI text)
        {
            m_AllInstance_UIFloatText.Remove(text);
            Destroy(text.gameObject);
        }
        private void Destroy_WorldText(TextMeshPro text)
        {
            m_AllInstance_WorldFloatText.Remove(text);
            Destroy(text.gameObject);
        }
        #endregion
        #region Public方法
        /// <summary>
        /// 初始化，
        /// 如果你没有主动进行初始化，在第一次使用的时候会自动初始化，
        /// 初始化可能需要一点时间，这会导致第一次使用时文字显示有延迟。
        /// </summary>
        /// <returns></returns>
        public static AsyncOperationHandle InitializeAsync(Camera camera = null)
        {
            return Instance.Initialize(camera);
        }
        /// <summary>
        /// 创建浮动的UI文本
        /// </summary>
        /// <param name="style"></param>
        /// <param name="content"></param>
        /// <param name="localPosition"></param>
        /// <param name="followTarget"></param>
        public static void CreatFloatTextInUI(TextStyle style, string content, Vector3 localPosition, Transform followTarget = null)
        {
            Instance.CreatFloatText(style, content, localPosition, followTarget, false);
        }
        /// <summary>
        /// 创建浮动的World文本
        /// </summary>
        /// <param name="style"></param>
        /// <param name="content"></param>
        /// <param name="localPosition"></param>
        /// <param name="followTarget"></param>
        public static void CreatFloatTextInWorld(TextStyle style, string content, Vector3 localPosition, Transform followTarget = null)
        {
            Instance.CreatFloatText(style, content, localPosition, followTarget, true);
        }
        /// <summary>
        /// 创建固定的UI文本
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fontSize"></param>
        /// <param name="localPosition"></param>
        /// <param name="followTarget"></param>
        public static void CreateEnternalTextInUI(string content, float fontSize, Vector3 localPosition, Transform followTarget = null)
        {
            Instance.CreateEnternalText(content, fontSize, localPosition, followTarget, false);
        }
        /// <summary>
        /// 创建固定的World文本
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fontSize"></param>
        /// <param name="localPosition"></param>
        /// <param name="followTarget"></param>
        public static void CreateEnternalTextInWorld(string content, float fontSize, Vector3 localPosition, Transform followTarget = null)
        {
            Instance.CreateEnternalText(content, fontSize, localPosition, followTarget, true);
        }
        /// <summary>
        /// 立刻回收所有文本实体，一般在切换场景时使用。
        /// </summary>
        public static void ReleseAllTextInstance()
        {
            Instance.ReleseAllText();
        }

        public Dictionary<string, int> GetInstanceCount()
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>(3)
            {
                {"UIText",m_AllInstance_UIFloatText.Count },
                {"WorldText",m_AllInstance_WorldFloatText.Count }
            };
            return keyValuePairs;
        }
        #endregion

        #region Private方法
        private AsyncOperationHandle Initialize(Camera camera = null)
        {
            if (m_Initialized) { return m_InitializeHandle; }
            m_Initialized = true;

            m_AllInstance_UIFloatText = new List<TextMeshProUGUI>(10);
            m_AllInstance_WorldFloatText = new List<TextMeshPro>(10);
            if (camera == null)
            {
                m_WorldTextLookAtCamera = Camera.main;//默认使用主摄像机
            }
            else
            {
                m_WorldTextLookAtCamera = camera;
            }
            /*
             * 创建根节点
             */
            GameObject root = new GameObject("UITextRoot");
            root.transform.parent = transform;
            Canvas canvas = root.AddComponent<Canvas>();
            canvas.renderMode = m_RenderMode;
            canvas.sortingOrder = m_SortOrder;
            CanvasScaler canvasScaler = root.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = m_ScaleMode;
            canvasScaler.referenceResolution = m_ScreenSize;
            m_UITextRoot = root.transform;

            root = new GameObject("WorldTextRoot");
            root.transform.parent = transform;
            m_WorldTextRoot = root.transform;

            /*
             * 读取资源，初始化
             */
            var handle = Addressables.LoadAssetAsync<TMP_FontAsset>(m_FontAssetAddres);
            m_InitializeHandle = handle;
            handle.Completed += (handle) =>
            {
                m_FontAsset = handle.Result;
                /*
                 * 创建样本
                 */
                m_TextTemplate_UI = new GameObject("TextTemplate_UI");
                m_TextTemplate_UI.transform.SetParent(m_UITextRoot);
                TextMeshProUGUI UIText = m_TextTemplate_UI.AddComponent<TextMeshProUGUI>();//会自动挂载RectTransform和canvas render
                UIText.enableWordWrapping = false;
                UIText.raycastTarget = false;
                UIText.verticalAlignment = VerticalAlignmentOptions.Baseline;
                RectTransform UIText_Transform = m_TextTemplate_UI.GetComponent<RectTransform>();
                UIText_Transform.localPosition = Vector3.zero;
                UIText_Transform.sizeDelta = Vector2.zero;
                UIText_Transform.anchorMax = Vector2.zero;
                UIText_Transform.anchorMin = Vector2.zero;
                UIText_Transform.pivot = Vector2.zero;
                m_TextTemplate_UI.SetActive(false);

                m_TextTemplate_World = new GameObject("TextTemplate_World");
                m_TextTemplate_World.transform.SetParent(m_WorldTextRoot);
                TextMeshPro worldText = m_TextTemplate_World.AddComponent<TextMeshPro>();
                worldText.enableWordWrapping = false;
                worldText.raycastTarget = false;
                worldText.verticalAlignment = VerticalAlignmentOptions.Baseline;
                RectTransform worldText_Transform = m_TextTemplate_World.GetComponent<RectTransform>();
                worldText_Transform.localPosition = Vector3.zero;
                worldText_Transform.position = Vector3.zero;
                worldText_Transform.sizeDelta = Vector2.zero;
                worldText_Transform.anchorMax = Vector2.zero;
                worldText_Transform.anchorMin = Vector2.zero;
                worldText_Transform.pivot = Vector2.zero;
                m_TextTemplate_World.SetActive(false);
                if (m_FontAsset != null)
                {
                    UIText.font = m_FontAsset;
                    worldText.font = m_FontAsset;
                }
                /*
                 * 初始化对象池
                 */
                m_UITextPool = new ObjectPool<TextMeshProUGUI>(Creat_UIText, Get_UIText, Release_UIText, Destroy_UIText);
                m_WorldTextPool = new ObjectPool<TextMeshPro>(Creat_WorldText, Get_WorldText, Release_WorldText, Destroy_WorldText);
            };
            return handle;
        }
        private void CreatFloatText(TextStyle style, string content, Vector3 localPosition, Transform followTarget, bool isWorldText)
        {
            StartCoroutine(CreatFloatTextCore(style, content, localPosition, followTarget, isWorldText));
        }
        private void ReleseAllText()
        {
            StopAllCoroutines();
            foreach (var item in m_AllInstance_UIFloatText)
            {
                if (item.gameObject.activeSelf == true)
                {
                    m_UITextPool.Release(item);
                }
            }
            foreach (var item in m_AllInstance_WorldFloatText)
            {
                if (item.gameObject.activeSelf == true)
                {
                    m_WorldTextPool.Release(item);
                }
            }
        }

        private IEnumerator CreatFloatTextCore(TextStyle style, string content, Vector3 localPosition, Transform followTarget, bool isWorldText)
        {
            if (!m_Initialized)//如果没有进行过初始化那么就初始化
            {
                Initialize();
            }
            while (!m_InitializeHandle.IsDone)//等待初始化完毕
            {
                yield return null;
            }
            TMP_Text text;
            if (isWorldText)
            {
                text = m_WorldTextPool.Get();
            }
            else
            {
                text = m_UITextPool.Get();
            }
            text.horizontalAlignment = style.alignment;
            text.fontSize = style.fontStartSize;
            text.color = style.fontColor;
            text.text = content;

            Vector3 worldPosition;//实例的世界坐标。

            float startTime = Time.time;
            float currentTime = 0;
            float finalTime = style.lifeTime;
            float animateTime = style.animateTime;
            Vector3 gravity = style.gravity;
            Vector3 currentVelocity = new Vector3(Random.Range(style.minVelocity.x, style.maxVelocity.x), Random.Range(style.minVelocity.y, style.maxVelocity.y), Random.Range(style.minVelocity.z, style.maxVelocity.z));
            Vector3 currentMovement = Vector3.zero;
            float startFadeTime = style.startFadeTime;
            float fontSizeScaleTime = style.fontSizeScaleTime;
            float fontStartSize = style.fontStartSize;
            float fontFinalSize = style.fontFinalSize;
            Color color = style.fontColor;


            float alphaLerp = 0;
            float alphaTimer = style.lifeTime - style.startFadeTime;

            float fontSizeChangeLerp = 0;

            while (currentTime < finalTime)
            {
                if (followTarget != null)
                {
                    worldPosition = followTarget.position + localPosition;
                }
                else
                {
                    worldPosition = localPosition;
                }
                if (currentTime < animateTime)
                {
                    currentVelocity += gravity * Time.deltaTime;
                    currentMovement += currentVelocity * Time.deltaTime;

                }
                worldPosition += currentMovement;
                if (currentTime > startFadeTime)
                {
                    alphaLerp += 1 * Time.deltaTime / alphaTimer;
                    text.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1, 0, alphaLerp));
                }
                if (currentTime < fontSizeScaleTime)
                {
                    fontSizeChangeLerp += 1 * Time.deltaTime / fontSizeScaleTime;
                    text.fontSize = Mathf.Lerp(fontStartSize, fontFinalSize, fontSizeChangeLerp);
                }

                if (isWorldText)
                {
                    text.transform.position = worldPosition;
                    text.transform.rotation = m_WorldTextLookAtCamera.transform.rotation;
                }
                else
                {
                    text.transform.position = m_WorldTextLookAtCamera.WorldToScreenPoint(worldPosition);
                }

                yield return null;
                currentTime = Time.time - startTime;
            }
            if (isWorldText)
            {
                m_WorldTextPool.Release(text as TextMeshPro);
            }
            else
            {
                m_UITextPool.Release(text as TextMeshProUGUI);
            }
        }
        private void CreateEnternalText(string content, float fontSize, Vector3 localPosition, Transform followTarget, bool isWorldText)
        {
            StartCoroutine(CreateEnternalTextCore(content, fontSize, localPosition, followTarget, isWorldText));
        }
        private IEnumerator CreateEnternalTextCore(string content, float fontSize, Vector3 localPosition, Transform followTarget, bool isWorldText)
        {
            if (!m_Initialized)//如果没有进行过初始化那么就初始化
            {
                Initialize();
            }
            while (!m_InitializeHandle.IsDone)//等待初始化完毕
            {
                yield return null;
            }
            bool haveFollowTarget = followTarget == null ? false : true;
            TMP_Text text;
            if (isWorldText)
            {
                text = m_WorldTextPool.Get();
            }
            else
            {
                text = m_UITextPool.Get();
            }
            text.fontSize = fontSize;
            text.text = content;
            text.horizontalAlignment = HorizontalAlignmentOptions.Geometry;
            Vector3 worldPosition = Vector3.zero;
            while (true)
            {
                if (haveFollowTarget)
                {
                    if (followTarget == null || !followTarget.gameObject.activeSelf)
                    {
                        if (isWorldText)
                        {
                            m_WorldTextPool.Release(text as TextMeshPro);
                        }
                        else
                        {
                            m_UITextPool.Release(text as TextMeshProUGUI);
                        }
                        yield break;
                    }
                    else
                    {
                        worldPosition = followTarget.position + localPosition;
                    }
                }
                else
                {
                    worldPosition = localPosition;
                }

                if (isWorldText)
                {

                    text.transform.position = worldPosition;
                    text.transform.rotation = m_WorldTextLookAtCamera.transform.rotation;
                }
                else
                {
                    text.transform.position = m_WorldTextLookAtCamera.WorldToScreenPoint(worldPosition);
                }
                yield return null;
            }


        }
        #endregion

        #region Unity方法
        #endregion
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(FloatText))]
    public class CustomEditor_FloatText : Editor
    {
        private FloatText m_FloatText;
        private void OnEnable()
        {
            m_FloatText = (FloatText)target;
        }
        public override void OnInspectorGUI()
        {
            Dictionary<string, int> keyValuePairs = m_FloatText.GetInstanceCount();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("UI文本数量", new GUILayoutOption[] { GUILayout.MaxWidth(100) });
            EditorGUILayout.LabelField(keyValuePairs["UIText"].ToString(), EditorStyles.largeLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("World文本数量", new GUILayoutOption[] { GUILayout.MaxWidth(100), });
            GUILayout.Label(keyValuePairs["WorldText"].ToString(), EditorStyles.largeLabel);
            EditorGUILayout.EndHorizontal();
        }
    }
#endif
    public class TextStyle
    {
        /// <summary>
        /// 文字的起始大小
        /// </summary>
        public float fontStartSize;
        /// <summary>
        /// 文字的最终大小
        /// </summary>
        public float fontFinalSize;
        /// <summary>
        /// 文字从起始大小变化到最终大小需要的时间
        /// </summary>
        public float fontSizeScaleTime;
        /// <summary>
        /// 文字的颜色
        /// </summary>
        public Color fontColor;
        /// <summary>
        /// 对齐方式
        ///  Left左对齐，Right右对齐，Center居中对齐（推荐使用Geometry代替,居的更中)，Flush文字挤在中间。
        /// </summary>
        public HorizontalAlignmentOptions alignment;
        /// <summary>
        /// 生命周期
        /// </summary>
        public float lifeTime;
        /// <summary>
        /// 文字可以活动的时间，超过此时间后文字将固定，不受力量影响。
        /// </summary>
        public float animateTime;
        /// <summary>
        /// 多久后文字开始透明直至消失。
        /// </summary>
        public float startFadeTime;
        /// <summary>
        /// 最小初始力量，系统会在最大和最小初始力量之间随机一个数值，设为该次文字显示的力量
        /// </summary>
        public Vector3 minVelocity;
        /// <summary>
        /// 最大初始力量，系统会在最大和最小初始力量之间随机一个数值，设为该次文字显示的力量
        /// </summary>
        public Vector3 maxVelocity;
        /// <summary>
        /// 如果有文字受到重力影响的需求，请修改此项。
        /// </summary>
        public Vector3 gravity;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontStartSize">文字的起始大小</param>
        /// <param name="fontFinalSize">文字的最终大小</param>
        /// <param name="fontSizeScaleTime">文字从起始大小变化到最终大小需要的时间</param>
        /// <param name="fontColor">文字的颜色</param>
        /// <param name="alignment">对齐方式</param>
        /// <param name="lifeTime">生命周期</param>
        /// <param name="animateTime">文字可以活动的时间，超过此时间后文字将固定，不受力量影响。</param>
        /// <param name="startFadeTime">多久后文字开始透明直至消失。</param>
        /// <param name="minVelocity">最小初始力量，系统会在最大和最小初始力量之间随机一个数值，设为该次文字显示的力量</param>
        /// <param name="maxVelocity">最大初始力量，系统会在最大和最小初始力量之间随机一个数值，设为该次文字显示的力量</param>
        /// <param name="gravity">如果有文字受到重力影响的需求，请修改此项。</param>
        public TextStyle(float fontStartSize, float fontFinalSize, float fontSizeScaleTime, Color fontColor, HorizontalAlignmentOptions alignment, float lifeTime, float animateTime, float startFadeTime, Vector3 minVelocity, Vector3 maxVelocity, Vector3 gravity)
        {
            this.fontStartSize = fontStartSize;
            this.fontFinalSize = fontFinalSize;
            this.fontSizeScaleTime = fontSizeScaleTime;
            this.fontColor = fontColor;
            this.alignment = alignment;
            this.lifeTime = lifeTime;
            this.animateTime = animateTime;
            this.startFadeTime = startFadeTime;
            this.minVelocity = minVelocity;
            this.maxVelocity = maxVelocity;
            this.gravity = gravity;
        }
    }

    public class TextStyleLibrary
    {
        public static TextStyle UIStyle_1 = new TextStyle(20f, 46f, 0.2f, Color.yellow, HorizontalAlignmentOptions.Geometry, 0.5f, 0.07f, 0.2f, new Vector3(-10f, 10f, 0f), new Vector3(10f, 10f, 0f), Vector3.zero);
        public static TextStyle UIStyle_2 = new TextStyle(20f, 46f, 0.2f, Color.yellow, HorizontalAlignmentOptions.Geometry, 1f, 0.5f, 0.5f, new Vector3(-5f, 10f, 0f), new Vector3(5f, 10f, 0f), new Vector3(0f, -50f, 0f));
        public static TextStyle UIStyle_3 = new TextStyle(46f, 46f, 0f, Color.yellow, HorizontalAlignmentOptions.Geometry, 1f, 0.7f, 0.5f, new Vector3(0f, 3f, 0f), new Vector3(0f, 3f, 0f), new Vector3(0f, -4f, 0f));
        public static TextStyle worldStyle_1 = new TextStyle(1f, 5f, 0.3f, Color.green, HorizontalAlignmentOptions.Geometry, 0.6f, 0.07f, 0.3f, new Vector3(0, 10, 0), new Vector3(0, 10, 0), Vector3.zero);
    }
}
