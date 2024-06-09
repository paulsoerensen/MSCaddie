#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
191,
199,
200,
201,
202,
203,
204,
205,
207,
208,
255,
256,
257,
281,
282,
283,
293,
294,
295,
296,
383,
384,
385,
388,
419,
420,
422,
424,
426,
428,
433,
441,
442,
443,
444,
445,
446,
447,
448,
449,
529,
530,
582,
588,
591,
593,
598,
599,
601,
602,
606,
607,
609,
611,
612,
615,
616,
617,
620,
623,
625,
627,
636,
691,
693,
695,
705,
706,
707,
709,
715,
716,
717,
718,
719,
727,
728,
729,
733,
734,
736,
738,
911,
1059,
1060,
6160,
6161,
6163,
6164,
6165,
6166,
6167,
6169,
6171,
6173,
6181,
6183,
6188,
6190,
6192,
6194,
6245,
6246,
6248,
6249,
6250,
6251,
6252,
6254,
6256,
7104,
7108,
7110,
7111,
7112,
7113,
7307,
7308,
7309,
7310,
7326,
7327,
7328,
7330,
7369,
7427,
7429,
7438,
7439,
7440,
7441,
7806,
7810,
7811,
7836,
7853,
7860,
7867,
7878,
7881,
7901,
7972,
7974,
7983,
7985,
7986,
7993,
8007,
8027,
8028,
8036,
8038,
8045,
8046,
8049,
8051,
8056,
8062,
8063,
8070,
8072,
8084,
8087,
8088,
8089,
8100,
8109,
8115,
8116,
8117,
8119,
8120,
8137,
8139,
8153,
8170,
8197,
8222,
8223,
8638,
8721,
8722,
8866,
8867,
8871,
8874,
8936,
9283,
9284,
9495,
9505,
10010,
10031,
10033,
10035,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
void ves_icall_System_Enum_InternalBoxEnum_raw (int,int,int64_t,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_ModF (double,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_System_RuntimeType_AllocateValueType_raw (int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 191,
ves_icall_System_Array_InternalCreate,
// token 199,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 200,
ves_icall_System_Array_CanChangePrimitive,
// token 201,
ves_icall_System_Array_FastCopy,
// token 202,
ves_icall_System_Array_GetLengthInternal_raw,
// token 203,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 204,
ves_icall_System_Array_GetGenericValue_icall,
// token 205,
ves_icall_System_Array_GetValueImpl_raw,
// token 207,
ves_icall_System_Array_SetValueImpl_raw,
// token 208,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 255,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 256,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 257,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 281,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 282,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 283,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 293,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 294,
ves_icall_System_Enum_InternalBoxEnum_raw,
// token 295,
ves_icall_System_Enum_InternalGetCorElementType,
// token 296,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 383,
ves_icall_System_Environment_get_ProcessorCount,
// token 384,
ves_icall_System_Environment_get_TickCount,
// token 385,
ves_icall_System_Environment_get_TickCount64,
// token 388,
ves_icall_System_Environment_FailFast_raw,
// token 419,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 420,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 422,
ves_icall_System_GC_SuppressFinalize_raw,
// token 424,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 426,
ves_icall_System_GC_GetGCMemoryInfo,
// token 428,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 433,
ves_icall_System_Object_MemberwiseClone_raw,
// token 441,
ves_icall_System_Math_Ceiling,
// token 442,
ves_icall_System_Math_Cos,
// token 443,
ves_icall_System_Math_Floor,
// token 444,
ves_icall_System_Math_Log10,
// token 445,
ves_icall_System_Math_Pow,
// token 446,
ves_icall_System_Math_Sin,
// token 447,
ves_icall_System_Math_Sqrt,
// token 448,
ves_icall_System_Math_Tan,
// token 449,
ves_icall_System_Math_ModF,
// token 529,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 530,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 582,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 588,
ves_icall_RuntimeType_make_array_type_raw,
// token 591,
ves_icall_RuntimeType_make_byref_type_raw,
// token 593,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 598,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 599,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 601,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 602,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 606,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 607,
ves_icall_System_RuntimeType_AllocateValueType_raw,
// token 609,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 611,
ves_icall_System_RuntimeType_getFullName_raw,
// token 612,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 615,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 616,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 617,
ves_icall_RuntimeType_GetFields_native_raw,
// token 620,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 623,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 625,
ves_icall_RuntimeType_GetName_raw,
// token 627,
ves_icall_RuntimeType_GetNamespace_raw,
// token 636,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 691,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 693,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 695,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 705,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 706,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 707,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 709,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 715,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 716,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 717,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 718,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 719,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 727,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 728,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 729,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 733,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 734,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 736,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 738,
ves_icall_System_String_FastAllocateString_raw,
// token 911,
ves_icall_System_Type_internal_from_handle_raw,
// token 1059,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1060,
ves_icall_System_ValueType_Equals_raw,
// token 6160,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 6161,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 6163,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 6164,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 6165,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 6166,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 6167,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 6169,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 6171,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 6173,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 6181,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 6183,
mono_monitor_exit_icall_raw,
// token 6188,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 6190,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 6192,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 6194,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 6245,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 6246,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 6248,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 6249,
ves_icall_System_Threading_Thread_GetState_raw,
// token 6250,
ves_icall_System_Threading_Thread_SetState_raw,
// token 6251,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 6252,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 6254,
ves_icall_System_Threading_Thread_YieldInternal,
// token 6256,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 7104,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 7108,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 7110,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 7111,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 7112,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 7113,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 7307,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 7308,
ves_icall_System_GCHandle_InternalFree_raw,
// token 7309,
ves_icall_System_GCHandle_InternalGet_raw,
// token 7310,
ves_icall_System_GCHandle_InternalSet_raw,
// token 7326,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 7327,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 7328,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 7330,
ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw,
// token 7369,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 7427,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 7429,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw,
// token 7438,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 7439,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 7440,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 7441,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 7806,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 7810,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 7811,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 7836,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 7853,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 7860,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 7867,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 7878,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 7881,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 7901,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 7972,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 7974,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 7983,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 7985,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 7986,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 7993,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 8007,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 8027,
ves_icall_reflection_get_token_raw,
// token 8028,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 8036,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 8038,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 8045,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 8046,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 8049,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 8051,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 8056,
ves_icall_reflection_get_token_raw,
// token 8062,
ves_icall_get_method_info_raw,
// token 8063,
ves_icall_get_method_attributes,
// token 8070,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 8072,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 8084,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 8087,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 8088,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 8089,
ves_icall_reflection_get_token_raw,
// token 8100,
ves_icall_InternalInvoke_raw,
// token 8109,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 8115,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 8116,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 8117,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 8119,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 8120,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 8137,
ves_icall_InvokeClassConstructor_raw,
// token 8139,
ves_icall_InternalInvoke_raw,
// token 8153,
ves_icall_reflection_get_token_raw,
// token 8170,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 8197,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 8222,
ves_icall_reflection_get_token_raw,
// token 8223,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 8638,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 8721,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 8722,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 8866,
ves_icall_ModuleBuilder_basic_init_raw,
// token 8867,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 8871,
ves_icall_ModuleBuilder_getToken_raw,
// token 8874,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 8936,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 9283,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 9284,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 9495,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 9505,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 10010,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 10031,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 10033,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 10035,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
4,
4,
0,
4,
4,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
};
