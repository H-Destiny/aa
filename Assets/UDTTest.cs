using UnityEngine;

using System.Collections;

using System.Collections.Generic;

using System.Linq;

using Vuforia;



public class UDTTest : MonoBehaviour, IUserDefinedTargetEventHandler

{

    UserDefinedTargetBuildingBehaviour mTargetBuildingBehaviour;

    ObjectTracker mObjectTracker;

    // 新定义的数据集添加到DataSet里

    DataSet mBuiltDataSet;

    //mFrameQuality 是一个用来记录当前帧图像质量的枚举类型
    ImageTargetBuilder.FrameQuality mFrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;

    int mTargetCounter;

    //声明一个公开的ImageTargetBehaviour然后在Unity中赋值

    public ImageTargetBehaviour ImageTargetTemplate;

    void Start()

    {

        mTargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();

        if (mTargetBuildingBehaviour)

        {
            mTargetBuildingBehaviour.RegisterEventHandler(this);
            Debug.Log("Registering User Defined Target event handler.");
        }

    }

    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        mFrameQuality = frameQuality;

        if (mFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW)

        {
            Debug.Log("Low camera image quality");
        }
    }

    public void OnInitialized()
    {
        mObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        if (mObjectTracker != null)

        {

            mBuiltDataSet = mObjectTracker.CreateDataSet();

            mObjectTracker.ActivateDataSet(mBuiltDataSet);

        }
    }

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        mTargetCounter++;

        // Deactivates the dataset first

        mObjectTracker.DeactivateDataSet(mBuiltDataSet);

        // Destroy the oldest target if the dataset is full or the dataset

        // already contains five user-defined targets.

        if (mBuiltDataSet.HasReachedTrackableLimit() || mBuiltDataSet.GetTrackables().Count() >= 5)

        {

            IEnumerable<Trackable> trackables = mBuiltDataSet.GetTrackables();

            Trackable oldest = null;

            foreach (Trackable trackable in trackables)

            {

                if (oldest == null || trackable.ID < oldest.ID)

                    oldest = trackable;

            }

            if (oldest != null)

            {

                Debug.Log("Destroying oldest trackable in UDT dataset: " + oldest.Name);

                mBuiltDataSet.Destroy(oldest, true);

            }

        }

        // Get predefined trackable and instantiate it

        ImageTargetBehaviour imageTargetCopy = (ImageTargetBehaviour)Instantiate(ImageTargetTemplate);

        imageTargetCopy.gameObject.name = "UserDefinedTarget-" + mTargetCounter;



        // Add the duplicated trackable to the data set and activate it

        mBuiltDataSet.CreateTrackable(trackableSource, imageTargetCopy.gameObject);



        // Activate the dataset again

        mObjectTracker.ActivateDataSet(mBuiltDataSet);
    }

    public void BuildNewTarget()
    {
        mTargetBuildingBehaviour.BuildNewTarget("test", 50);
    }

}
