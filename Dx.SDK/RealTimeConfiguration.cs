// Decompiled with JetBrains decompiler
// Type: Dx.SDK.RealTimeConfiguration
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using info.lundin.Math;
using log4net;
using System;
using System.Collections;
using System.Reflection;

namespace Dx.SDK
{
    public class RealTimeConfiguration
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(RealTimeConfiguration));
        private ExpressionParser parser;
        private bool enableCustomFunctions;
        private RealTimeFunction mapFunction;
        private RealTimeFunction tpsFunction;
        private RealTimeFunction airTempFunction;
        private RealTimeFunction engineTempFunction;
        private RealTimeFunction ignitionFinalFunction;
        private RealTimeFunction injectionCorrectedFunction;
        private RealTimeFunction injectorDutyFunction;

        public RealTimeConfiguration() => this.parser = new ExpressionParser();

        public bool EnableCustomFunctions
        {
            get => this.enableCustomFunctions;
            set => this.enableCustomFunctions = value;
        }

        public RealTimeFunction MapFunction
        {
            get => this.mapFunction;
            set => this.mapFunction = value;
        }

        public double getMapFunctionValue(int inValue)
        {
            try
            {
                if (!this.enableCustomFunctions)
                    return (double)inValue;
                double mapFunctionValue = this.parser.Parse(this.mapFunction.Expression, new Hashtable()
        {
          {
            (object) "x",
            (object) inValue.ToString()
          }
        });
                RealTimeConfiguration.logger.Debug((object)("Expression:" + this.mapFunction.Expression + " InValue=" + inValue.ToString() + " evalValue=" + mapFunctionValue.ToString()));
                return mapFunctionValue;
            }
            catch (Exception ex)
            {
                if (RealTimeConfiguration.logger.IsErrorEnabled)
                    RealTimeConfiguration.logger.Error((object)"Error in getMapFunctionValue", ex);
                return (double)inValue;
            }
        }

        public RealTimeFunction TpsFunction
        {
            get => this.tpsFunction;
            set => this.tpsFunction = value;
        }

        public double getTpsFunctionValue(int inValue)
        {
            try
            {
                if (!this.enableCustomFunctions)
                    return (double)inValue;
                return this.parser.Parse(this.tpsFunction.Expression, new Hashtable()
        {
          {
            (object) "x",
            (object) inValue.ToString()
          }
        });
            }
            catch (Exception ex)
            {
                if (RealTimeConfiguration.logger.IsErrorEnabled)
                    RealTimeConfiguration.logger.Error((object)"Error in get tpc function value", ex);
                return (double)inValue;
            }
        }

        public RealTimeFunction AirTempFunction
        {
            get => this.airTempFunction;
            set => this.airTempFunction = value;
        }

        public double getAirTempFunctionValue(int inValue)
        {
            try
            {
                if (!this.enableCustomFunctions)
                    return (double)inValue;
                return this.parser.Parse(this.airTempFunction.Expression, new Hashtable()
        {
          {
            (object) "x",
            (object) inValue.ToString()
          }
        });
            }
            catch (Exception ex)
            {
                if (RealTimeConfiguration.logger.IsErrorEnabled)
                    RealTimeConfiguration.logger.Error((object)"Error in getAirTempFunctionValue", ex);
                return (double)inValue;
            }
        }

        public RealTimeFunction EngineTempFunction
        {
            get => this.engineTempFunction;
            set => this.engineTempFunction = value;
        }

        public double getEngineTempFunctionValue(int inValue)
        {
            try
            {
                if (!this.enableCustomFunctions)
                    return (double)inValue;
                return this.parser.Parse(this.engineTempFunction.Expression, new Hashtable()
        {
          {
            (object) "x",
            (object) inValue.ToString()
          }
        });
            }
            catch (Exception ex)
            {
                if (RealTimeConfiguration.logger.IsErrorEnabled)
                    RealTimeConfiguration.logger.Error((object)"Error in getEngineTempFunctionValue", ex);
                return (double)inValue;
            }
        }

        public RealTimeFunction IgnitionFinalFunction
        {
            get => this.ignitionFinalFunction;
            set => this.ignitionFinalFunction = value;
        }

        public double getIgnitionFinalFunctionValue(int inValue)
        {
            try
            {
                if (!this.enableCustomFunctions)
                    return (double)inValue;
                return this.parser.Parse(this.ignitionFinalFunction.Expression, new Hashtable()
        {
          {
            (object) "x",
            (object) inValue.ToString()
          }
        });
            }
            catch (Exception ex)
            {
                if (RealTimeConfiguration.logger.IsErrorEnabled)
                    RealTimeConfiguration.logger.Error((object)"Error in getIgnitionFinalFunctionValue", ex);
                return (double)inValue;
            }
        }

        public RealTimeFunction InjectionCorrectedFunction
        {
            get => this.injectionCorrectedFunction;
            set => this.injectionCorrectedFunction = value;
        }

        public double getInjectionCorrectedFunctionValue(int inValue)
        {
            try
            {
                if (!this.enableCustomFunctions)
                    return (double)inValue;
                return this.parser.Parse(this.injectionCorrectedFunction.Expression, new Hashtable()
        {
          {
            (object) "x",
            (object) inValue.ToString()
          }
        });
            }
            catch (Exception ex)
            {
                if (RealTimeConfiguration.logger.IsErrorEnabled)
                    RealTimeConfiguration.logger.Error((object)"Error in getInjectionCorrectedFunctionValue", ex);
                return (double)inValue;
            }
        }

        public RealTimeFunction InjectorDutyFunction
        {
            get => this.injectorDutyFunction;
            set => this.injectorDutyFunction = value;
        }

        public double getInjectorDutyFunctionValue(int inValue)
        {
            try
            {
                if (!this.enableCustomFunctions)
                    return (double)inValue;
                return this.parser.Parse(this.injectorDutyFunction.Expression, new Hashtable()
        {
          {
            (object) "x",
            (object) inValue.ToString()
          }
        });
            }
            catch (Exception ex)
            {
                if (RealTimeConfiguration.logger.IsErrorEnabled)
                    RealTimeConfiguration.logger.Error((object)"Error in getInjectorDutyFunctionValue", ex);
                return (double)inValue;
            }
        }
    }
}
